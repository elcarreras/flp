using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace filp
{
    public partial class Form1 : Form
    {
        // состояния
        private readonly List<Attachment> _attachments = new();
        private int  _currentAttIndex = -1;
        private bool _loadingAtt      = false;

        public Form1()
        {
            InitializeComponent();
            txtDate.Text = DateTime.Today.ToString("dd.MM.yyyy");

            string defaultTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.docx");
            if (File.Exists(defaultTemplate))
                txtTemplatePath.Text = defaultTemplate;

            UpdateAttachmentButtons();
        }

        // обработчики
        private void btnBrowseTemplate_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Word документы (*.docx)|*.docx",
                Title  = "Выберите шаблон письма"
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
                "Настоящим сообщаем Вам, что в ответ на Ваш запрос нами подготовлен " +
                "полный пакет запрашиваемых документов. Просим ознакомиться с прилагаемыми " +
                "материалами и направить обратную связь в срок до " +
                DateTime.Today.AddDays(14).ToString("dd.MM.yyyy") + ".\n" +
                "    По всем возникающим вопросам просим обращаться по контактным данным, " +
                "указанным в шапке настоящего письма.";
            txtSenderPost.Text = "Директор";
            txtSenderName.Text = "Петров Пётр Петрович";

            _attachments.Clear();
            _currentAttIndex = -1;
            _attachments.Add(new Attachment
            {
                Title = "Перечень запрашиваемых документов",
                Pages = 2,
                Text  = "В соответствии с запросом от " + DateTime.Today.ToString("dd.MM.yyyy") +
                        " представляем перечень документов, подлежащих предоставлению:\n" +
                        "    1. Копия устава организации, заверенная в установленном порядке.\n" +
                        "    2. Выписка из ЕГРЮЛ, выданная не ранее чем за 30 дней до даты обращения.\n" +
                        "    3. Бухгалтерская отчётность за последний отчётный период.\n" +
                        "    Все документы должны быть представлены в срок до " +
                        DateTime.Today.AddDays(10).ToString("dd.MM.yyyy") + "."
            });
            _attachments.Add(new Attachment
            {
                Title = "Форма обратной связи",
                Pages = 1,
                Text  = "Настоящая форма предназначена для направления обратной связи по " +
                        "полученным материалам. Просим заполнить форму и направить её по " +
                        "электронному адресу, указанному в шапке письма.\n" +
                        "    Ваши комментарии и предложения будут рассмотрены в течение 5 рабочих дней."
            });
            RefreshList();
            lstAttachments.SelectedIndex = 0;

            lblStatus.Text = "Поля заполнены образцом.";
            lblStatus.ForeColor = SystemColors.ControlText;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTemplatePath.Text) || !File.Exists(txtTemplatePath.Text))
            {
                MessageBox.Show("Укажите корректный путь к шаблону.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveCurrentAttachment();

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
                GenerateLetter(saveDlg.FileName, replacements, _attachments);

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

        private void btnAddAtt_Click(object sender, EventArgs e)
        {
            SaveCurrentAttachment();
            var att = new Attachment { Title = "Новое приложение", Pages = 1 };
            _attachments.Add(att);
            lstAttachments.Items.Add(att.ToString());
            lstAttachments.SelectedIndex = lstAttachments.Items.Count - 1;
        }

        private void btnRemoveAtt_Click(object sender, EventArgs e)
        {
            int idx = lstAttachments.SelectedIndex;
            if (idx < 0) return;

            _currentAttIndex = -1;
            _attachments.RemoveAt(idx);
            lstAttachments.Items.RemoveAt(idx);

            if (lstAttachments.Items.Count > 0)
                lstAttachments.SelectedIndex = Math.Min(idx, lstAttachments.Items.Count - 1);
            else
                ClearAttachmentFields();

            UpdateAttachmentButtons();
        }

        private void btnAttUp_Click(object sender, EventArgs e)
        {
            int idx = lstAttachments.SelectedIndex;
            if (idx <= 0) return;
            SaveCurrentAttachment();
            MoveAttachment(idx, idx - 1);
        }

        private void btnAttDown_Click(object sender, EventArgs e)
        {
            int idx = lstAttachments.SelectedIndex;
            if (idx < 0 || idx >= _attachments.Count - 1) return;
            SaveCurrentAttachment();
            MoveAttachment(idx, idx + 1);
        }

        private void MoveAttachment(int from, int to)
        {
            var att = _attachments[from];
            _attachments.RemoveAt(from);
            _attachments.Insert(to, att);
            _currentAttIndex = -1;
            RefreshList();
            lstAttachments.SelectedIndex = to;
        }

        private void lstAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingAtt) return;
            SaveCurrentAttachment();

            _currentAttIndex = lstAttachments.SelectedIndex;
            if (_currentAttIndex >= 0 && _currentAttIndex < _attachments.Count)
            {
                _loadingAtt = true;
                var att = _attachments[_currentAttIndex];
                txtAttTitle.Text  = att.Title;
                numAttPages.Value = Math.Max(1, Math.Min(999, att.Pages));
                txtAttText.Text   = att.Text;
                _loadingAtt = false;
            }
            UpdateAttachmentButtons();
        }

        private void SaveCurrentAttachment()
        {
            if (_currentAttIndex < 0 || _currentAttIndex >= _attachments.Count) return;
            _attachments[_currentAttIndex].Title = txtAttTitle.Text;
            _attachments[_currentAttIndex].Pages = (int)numAttPages.Value;
            _attachments[_currentAttIndex].Text  = txtAttText.Text;

            _loadingAtt = true;
            lstAttachments.Items[_currentAttIndex] = _attachments[_currentAttIndex].ToString();
            _loadingAtt = false;
        }

        private void RefreshList()
        {
            lstAttachments.Items.Clear();
            foreach (var a in _attachments)
                lstAttachments.Items.Add(a.ToString());
        }

        private void ClearAttachmentFields()
        {
            _loadingAtt = true;
            txtAttTitle.Text  = "";
            numAttPages.Value = 1;
            txtAttText.Text   = "";
            _loadingAtt = false;
        }

        private void UpdateAttachmentButtons()
        {
            bool hasSelection = lstAttachments.SelectedIndex >= 0;
            btnRemoveAtt.Enabled = hasSelection;
            btnAttUp.Enabled     = lstAttachments.SelectedIndex > 0;
            btnAttDown.Enabled   = hasSelection && lstAttachments.SelectedIndex < lstAttachments.Items.Count - 1;
            grpAttEdit.Enabled   = hasSelection;
        }


        // генерация дока
        private static void GenerateLetter(string filePath,
            Dictionary<string, string> replacements,
            List<Attachment> attachments)
        {
            using var doc = WordprocessingDocument.Open(filePath, isEditable: true);
            var mainPart = doc.MainDocumentPart
                ?? throw new InvalidOperationException("Отсутствует основная часть документа.");
            var body = mainPart.Document?.Body
                ?? throw new InvalidOperationException("Документ не содержит тела.");

            ReplacePlaceholders(body, replacements);

            if (attachments.Count > 0)
            {
                InsertAttachmentList(body, attachments);
                AddAttachmentPages(body, attachments);
            }

            mainPart.Document.Save();
        }

        private static void ReplacePlaceholders(OpenXmlElement element,
            Dictionary<string, string> replacements)
        {
            foreach (var para in element.Descendants<Paragraph>())
            {
                string fullText = string.Concat(para.Descendants<Text>().Select(t => t.Text));
                if (!replacements.Keys.Any(p => fullText.Contains(p))) continue;

                string newText = fullText;
                foreach (var kv in replacements)
                    newText = newText.Replace(kv.Key, kv.Value);

                var runs = para.Elements<Run>().ToList();
                if (runs.Count == 0) continue;

                foreach (var t in runs[0].Elements<Text>().ToList()) t.Remove();
                runs[0].Append(new Text(newText) { Space = SpaceProcessingModeValues.Preserve });
                for (int i = 1; i < runs.Count; i++) runs[i].Remove();
            }
        }

        private static void InsertAttachmentList(Body body, List<Attachment> attachments)
        {
            var lastTable = body.Elements<Table>().LastOrDefault();
            if (lastTable == null) return;

            bool single = attachments.Count == 1;

            lastTable.InsertBeforeSelf(MakeEmptyParagraph());
            lastTable.InsertBeforeSelf(MakeParagraph(single ? "Приложение:" : "Приложения:"));

            for (int i = 0; i < attachments.Count; i++)
            {
                var att = attachments[i];
                string pagesStr = att.Pages > 0 ? $" на {att.Pages} л." : "";
                string line = single
                    ? att.Title + pagesStr
                    : $"{i + 1}. {att.Title}{pagesStr}";
                lastTable.InsertBeforeSelf(MakeParagraph(line));
            }

            lastTable.InsertBeforeSelf(MakeEmptyParagraph());
        }

        private static void AddAttachmentPages(Body body, List<Attachment> attachments)
        {
            var anchor = body.Elements<Paragraph>().LastOrDefault()
                ?? throw new InvalidOperationException("Не найден завершающий абзац тела документа.");

            for (int i = 0; i < attachments.Count; i++)
            {
                var att   = attachments[i];
                string label = attachments.Count == 1 ? "Приложение" : $"Приложение {i + 1}";

                anchor.InsertBeforeSelf(new Paragraph(
                    new ParagraphProperties(new SpacingBetweenLines { After = "0" }),
                    new Run(new Break { Type = BreakValues.Page })
                ));
                anchor.InsertBeforeSelf(MakeParagraph(label, JustificationValues.Right));
                anchor.InsertBeforeSelf(MakeEmptyParagraph());
                anchor.InsertBeforeSelf(MakeTitleParagraph(att.Title));
                anchor.InsertBeforeSelf(MakeEmptyParagraph());
                anchor.InsertBeforeSelf(MakeBodyParagraph(att.Text));
            }
        }

        private static RunProperties BaseRunProps(bool bold = false)
        {
            var rPr = new RunProperties(
                new RunFonts { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" },
                new FontSize { Val = "24" },
                new FontSizeComplexScript { Val = "24" }
            );
            if (bold) rPr.Append(new Bold());
            return rPr;
        }

        private static Paragraph MakeEmptyParagraph()
        {
            var p = new Paragraph();
            p.Append(new ParagraphProperties(
                new SpacingBetweenLines { After = "0", Line = "276", LineRule = LineSpacingRuleValues.Auto }
            ));
            return p;
        }

        private static Paragraph MakeParagraph(string text) =>
            MakeParagraph(text, JustificationValues.Left);

        private static Paragraph MakeParagraph(string text, JustificationValues jc)
        {
            var p = new Paragraph();
            p.Append(new ParagraphProperties(
                new SpacingBetweenLines { After = "0", Line = "276", LineRule = LineSpacingRuleValues.Auto },
                new Justification { Val = jc }
            ));
            var r = new Run();
            r.Append(BaseRunProps());
            r.Append(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
            p.Append(r);
            return p;
        }

        private static Paragraph MakeTitleParagraph(string text)
        {
            var p = new Paragraph();
            p.Append(new ParagraphProperties(
                new SpacingBetweenLines { After = "0", Line = "276", LineRule = LineSpacingRuleValues.Auto },
                new Justification { Val = JustificationValues.Center }
            ));
            var r = new Run();
            r.Append(BaseRunProps(bold: true));
            r.Append(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
            p.Append(r);
            return p;
        }

        private static Paragraph MakeBodyParagraph(string text)
        {
            var p = new Paragraph();
            p.Append(new ParagraphProperties(
                new SpacingBetweenLines { After = "0", Line = "360", LineRule = LineSpacingRuleValues.Auto },
                new Indentation { FirstLine = "709" },
                new Justification { Val = JustificationValues.Both }
            ));
            var r = new Run();
            r.Append(BaseRunProps());
            r.Append(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
            p.Append(r);
            return p;
        }
    }
}
