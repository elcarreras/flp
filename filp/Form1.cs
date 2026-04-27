using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace filp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtDate.Text = DateTime.Today.ToString("dd.MM.yyyy");

            string defaultTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.docx");
            if (File.Exists(defaultTemplate))
                txtTemplatePath.Text = defaultTemplate;
        }

        private void btnBrowseTemplate_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Word документы (*.docx)|*.docx",
                Title = "Выберите шаблон письма"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                txtTemplatePath.Text = dlg.FileName;
        }

        private void btnFillDefaults_Click(object sender, EventArgs e)
        {
            txtDate.Text          = DateTime.Today.ToString("dd.MM.yyyy");
            txtLetterNum.Text     = "1";
            txtRecipientPost.Text = "Генеральному директору";
            txtRecipientOrg.Text  = "ООО \"Ромашка\"";
            txtRecipientName.Text = "Иванову Ивану Ивановичу";
            txtGreetingName.Text  = "Иван Иванович";
            txtLetterSubject.Text = "О предоставлении документов";
            txtLetterBody.Text    =
                "Настоящим сообщаем Вам, что в ответ на Ваш запрос " +
                "нами подготовлен полный пакет запрашиваемых документов. " +
                "Просим ознакомиться с прилагаемыми материалами и " +
                "направить обратную связь в срок до " +
                DateTime.Today.AddDays(14).ToString("dd.MM.yyyy") + ".\n" +
                "    По всем возникающим вопросам просим обращаться " +
                "по контактным данным, указанным в шапке настоящего письма.";
            txtSenderPost.Text = "Директор";
            txtSenderName.Text = "Петров Пётр Петрович";

            lblStatus.Text = "Поля заполнены образцом.";
            lblStatus.ForeColor = System.Drawing.Color.Gray;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTemplatePath.Text) || !File.Exists(txtTemplatePath.Text))
            {
                MessageBox.Show("Укажите корректный путь к шаблону.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var saveDlg = new SaveFileDialog
            {
                Filter   = "Word документы (*.docx)|*.docx",
                Title    = "Сохранить письмо как...",
                FileName = $"Письмо_{DateTime.Now:yyyyMMdd_HHmmss}.docx"
            };

            if (saveDlg.ShowDialog() != DialogResult.OK)
                return;

            var replacements = new Dictionary<string, string>
            {
                ["{DATE}"]           = txtDate.Text,
                ["{LETTER_NUM}"]     = txtLetterNum.Text,
                ["{RECIPIENT_POST}"] = txtRecipientPost.Text,
                ["{RECIPIENT_ORG}"]  = txtRecipientOrg.Text,
                ["{RECIPIENT_NAME}"] = txtRecipientName.Text,
                ["{GREETING_NAME}"]  = txtGreetingName.Text,
                ["{LETTER_SUBJECT}"] = txtLetterSubject.Text,
                ["{LETTER_BODY}"]    = txtLetterBody.Text,
                ["{SENDER_POST}"]    = txtSenderPost.Text,
                ["{SENDER_NAME}"]    = txtSenderName.Text,
            };

            try
            {
                File.Copy(txtTemplatePath.Text, saveDlg.FileName, overwrite: true);
                GenerateLetter(saveDlg.FileName, replacements);

                lblStatus.Text = $"✓ Сохранено: {Path.GetFileName(saveDlg.FileName)}";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                if (MessageBox.Show("Письмо создано. Открыть файл?", "Готово",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveDlg.FileName)
                    {
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка при создании письма.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void GenerateLetter(string filePath, Dictionary<string, string> replacements)
        {
            using var doc = WordprocessingDocument.Open(filePath, isEditable: true);
            var mainPart = doc.MainDocumentPart
                ?? throw new InvalidOperationException("Отсутствует основная часть документа.");
            var body = mainPart.Document?.Body
                ?? throw new InvalidOperationException("Документ не содержит тела.");
            ReplacePlaceholders(body, replacements);
            mainPart.Document.Save();
        }

        private static void ReplacePlaceholders(OpenXmlElement element, Dictionary<string, string> replacements)
        {
            foreach (var para in element.Descendants<Paragraph>())
            {
                string fullText = string.Concat(para.Descendants<Text>().Select(t => t.Text));

                if (!replacements.Keys.Any(p => fullText.Contains(p)))
                    continue;

                string newText = fullText;
                foreach (var kv in replacements)
                    newText = newText.Replace(kv.Key, kv.Value);

                var runs = para.Elements<Run>().ToList();
                if (runs.Count == 0)
                    continue;

                var firstRun = runs[0];
                foreach (var t in firstRun.Elements<Text>().ToList())
                    t.Remove();

                firstRun.Append(new Text(newText) { Space = SpaceProcessingModeValues.Preserve });

                for (int i = 1; i < runs.Count; i++)
                    runs[i].Remove();
            }
        }
    }
}
