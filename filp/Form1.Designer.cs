namespace filp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTemplate    = new Panel();
            this.lblTemplatePath  = new Label();
            this.txtTemplatePath  = new TextBox();
            this.btnBrowseTemplate = new Button();

            this.grpRecipient     = new GroupBox();
            this.lblRecipientPost = new Label();
            this.txtRecipientPost = new TextBox();
            this.lblRecipientOrg  = new Label();
            this.txtRecipientOrg  = new TextBox();
            this.lblRecipientName = new Label();
            this.txtRecipientName = new TextBox();
            this.lblGreetingName  = new Label();
            this.txtGreetingName  = new TextBox();

            this.grpLetter        = new GroupBox();
            this.lblDate          = new Label();
            this.txtDate          = new TextBox();
            this.lblLetterNum     = new Label();
            this.txtLetterNum     = new TextBox();
            this.lblLetterSubject = new Label();
            this.txtLetterSubject = new TextBox();
            this.lblLetterBody    = new Label();
            this.txtLetterBody    = new TextBox();

            this.grpSender    = new GroupBox();
            this.lblSenderPost = new Label();
            this.txtSenderPost = new TextBox();
            this.lblSenderName = new Label();
            this.txtSenderName = new TextBox();

            this.btnFillDefaults = new Button();
            this.btnGenerate     = new Button();
            this.lblStatus       = new Label();

            this.SuspendLayout();

            // Form
            this.Text = "Генератор деловых писем";
            this.Size = new Size(680, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // panelTemplate
            this.panelTemplate.Dock = DockStyle.Top;
            this.panelTemplate.Height = 46;

            this.lblTemplatePath.Text = "Файл шаблона:";
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Location = new Point(8, 15);

            this.txtTemplatePath.Location = new Point(100, 12);
            this.txtTemplatePath.Width = 450;
            this.txtTemplatePath.ReadOnly = true;

            this.btnBrowseTemplate.Text = "Обзор...";
            this.btnBrowseTemplate.Location = new Point(558, 11);
            this.btnBrowseTemplate.Size = new Size(100, 23);
            this.btnBrowseTemplate.Click += new EventHandler(this.btnBrowseTemplate_Click);

            this.panelTemplate.Controls.AddRange(new Control[] {
                this.lblTemplatePath, this.txtTemplatePath, this.btnBrowseTemplate
            });

            // grpRecipient
            this.grpRecipient.Text = "Получатель";
            this.grpRecipient.Location = new Point(8, 52);
            this.grpRecipient.Size = new Size(648, 158);

            AddField(this.grpRecipient, this.lblRecipientPost, "Должность:",       this.txtRecipientPost, 22);
            AddField(this.grpRecipient, this.lblRecipientOrg,  "Организация:",      this.txtRecipientOrg,  52);
            AddField(this.grpRecipient, this.lblRecipientName, "ФИО (дат. пад.):", this.txtRecipientName, 82);
            AddField(this.grpRecipient, this.lblGreetingName,  "Обращение:",        this.txtGreetingName,  112);

            // grpLetter
            this.grpLetter.Text = "Письмо";
            this.grpLetter.Location = new Point(8, 218);
            this.grpLetter.Size = new Size(648, 262);

            AddField(this.grpLetter, this.lblDate,         "Дата:",  this.txtDate,         22);
            AddField(this.grpLetter, this.lblLetterNum,    "Номер:", this.txtLetterNum,     52);
            AddField(this.grpLetter, this.lblLetterSubject,"Тема:",  this.txtLetterSubject, 82);

            this.lblLetterBody.Text = "Текст:";
            this.lblLetterBody.AutoSize = true;
            this.lblLetterBody.Location = new Point(10, 117);

            this.txtLetterBody.Location = new Point(160, 114);
            this.txtLetterBody.Size = new Size(478, 134);
            this.txtLetterBody.Multiline = true;
            this.txtLetterBody.ScrollBars = ScrollBars.Vertical;
            this.grpLetter.Controls.AddRange(new Control[] { this.lblLetterBody, this.txtLetterBody });

            // grpSender
            this.grpSender.Text = "Отправитель";
            this.grpSender.Location = new Point(8, 488);
            this.grpSender.Size = new Size(648, 92);

            AddField(this.grpSender, this.lblSenderPost, "Должность:", this.txtSenderPost, 22);
            AddField(this.grpSender, this.lblSenderName, "ФИО:",       this.txtSenderName, 52);

            // btnFillDefaults
            this.btnFillDefaults.Text = "Заполнить образцом";
            this.btnFillDefaults.Location = new Point(8, 592);
            this.btnFillDefaults.Size = new Size(150, 26);
            this.btnFillDefaults.Click += new EventHandler(this.btnFillDefaults_Click);

            // btnGenerate
            this.btnGenerate.Text = "Создать письмо";
            this.btnGenerate.Location = new Point(168, 592);
            this.btnGenerate.Size = new Size(130, 26);
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);

            // lblStatus
            this.lblStatus.Location = new Point(308, 596);
            this.lblStatus.Size = new Size(350, 18);
            this.lblStatus.AutoSize = false;
            this.lblStatus.Text = "";

            this.Controls.AddRange(new Control[] {
                this.panelTemplate,
                this.grpRecipient,
                this.grpLetter,
                this.grpSender,
                this.btnFillDefaults,
                this.btnGenerate,
                this.lblStatus
            });

            this.ResumeLayout(false);
        }

        private static void AddField(Control parent, Label lbl, string labelText, TextBox txt, int y)
        {
            lbl.Text = labelText;
            lbl.AutoSize = true;
            lbl.Location = new Point(10, y + 3);

            txt.Location = new Point(160, y);
            txt.Width = 478;

            parent.Controls.AddRange(new Control[] { lbl, txt });
        }

        private Panel    panelTemplate;
        private Label    lblTemplatePath;
        private TextBox  txtTemplatePath;
        private Button   btnBrowseTemplate;

        private GroupBox grpRecipient;
        private Label    lblRecipientPost;
        private TextBox  txtRecipientPost;
        private Label    lblRecipientOrg;
        private TextBox  txtRecipientOrg;
        private Label    lblRecipientName;
        private TextBox  txtRecipientName;
        private Label    lblGreetingName;
        private TextBox  txtGreetingName;

        private GroupBox grpLetter;
        private Label    lblDate;
        private TextBox  txtDate;
        private Label    lblLetterNum;
        private TextBox  txtLetterNum;
        private Label    lblLetterSubject;
        private TextBox  txtLetterSubject;
        private Label    lblLetterBody;
        private TextBox  txtLetterBody;

        private GroupBox grpSender;
        private Label    lblSenderPost;
        private TextBox  txtSenderPost;
        private Label    lblSenderName;
        private TextBox  txtSenderName;

        private Button btnFillDefaults;
        private Button btnGenerate;
        private Label  lblStatus;
    }
}
