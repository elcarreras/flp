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
            this.tabControl       = new TabControl();
            this.tabMain          = new TabPage();
            this.tabAttachments   = new TabPage();

            this.grpTemplate      = new GroupBox();
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

            this.grpSender        = new GroupBox();
            this.lblSenderPost    = new Label();
            this.txtSenderPost    = new TextBox();
            this.lblSenderName    = new Label();
            this.txtSenderName    = new TextBox();

            this.lblAttList       = new Label();
            this.lstAttachments   = new ListBox();
            this.btnAddAtt        = new Button();
            this.btnRemoveAtt     = new Button();
            this.btnAttUp         = new Button();
            this.btnAttDown       = new Button();

            this.grpAttEdit       = new GroupBox();
            this.lblAttTitle      = new Label();
            this.txtAttTitle      = new TextBox();
            this.lblAttPages      = new Label();
            this.numAttPages      = new NumericUpDown();
            this.lblAttText       = new Label();
            this.txtAttText       = new TextBox();

            this.pnlBottom        = new Panel();
            this.btnFillDefaults  = new Button();
            this.btnGenerate      = new Button();
            this.lblStatus        = new Label();

            this.pnlPreview       = new Panel();
            this.lblPreviewTitle  = new Label();
            this.wbPreview        = new Microsoft.Web.WebView2.WinForms.WebView2();

            this.SuspendLayout();

            this.Text = "Генератор деловых писем";
            this.Size = new Size(1130, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 52;

            this.btnFillDefaults.Text = "Заполнить образцом";
            this.btnFillDefaults.Location = new Point(12, 12);
            this.btnFillDefaults.Size = new Size(148, 28);
            this.btnFillDefaults.Click += new EventHandler(this.btnFillDefaults_Click);

            this.btnGenerate.Text = "Создать письмо";
            this.btnGenerate.Location = new Point(168, 12);
            this.btnGenerate.Size = new Size(140, 28);
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);

            this.lblStatus.Location = new Point(320, 17);
            this.lblStatus.Size = new Size(360, 18);
            this.lblStatus.AutoSize = false;

            this.pnlBottom.Controls.AddRange(new Control[] {
                this.btnFillDefaults, this.btnGenerate, this.lblStatus
            });

            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Controls.AddRange(new TabPage[] {
                this.tabMain, this.tabAttachments
            });

            this.tabMain.Text = "Основное";
            this.tabMain.Padding = new Padding(4);

            this.tabAttachments.Text = "Приложения";
            this.tabAttachments.Padding = new Padding(4);

            this.grpTemplate.Text = "Шаблон";
            this.grpTemplate.Location = new Point(4, 4);
            this.grpTemplate.Size = new Size(648, 48);

            this.lblTemplatePath.Text = "Файл:";
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Location = new Point(8, 18);

            this.txtTemplatePath.Location = new Point(48, 15);
            this.txtTemplatePath.Width = 490;
            this.txtTemplatePath.ReadOnly = true;

            this.btnBrowseTemplate.Text = "Обзор...";
            this.btnBrowseTemplate.Location = new Point(546, 14);
            this.btnBrowseTemplate.Size = new Size(92, 23);
            this.btnBrowseTemplate.Click += new EventHandler(this.btnBrowseTemplate_Click);

            this.grpTemplate.Controls.AddRange(new Control[] {
                this.lblTemplatePath, this.txtTemplatePath, this.btnBrowseTemplate
            });

            this.grpRecipient.Text = "Получатель";
            this.grpRecipient.Location = new Point(4, 58);
            this.grpRecipient.Size = new Size(648, 150);

            AddField(grpRecipient, lblRecipientPost, "Должность:",       txtRecipientPost, 24);
            AddField(grpRecipient, lblRecipientOrg,  "Организация:",      txtRecipientOrg,  54);
            AddField(grpRecipient, lblRecipientName, "ФИО (дат. пад.):", txtRecipientName, 84);
            AddField(grpRecipient, lblGreetingName,  "Обращение:",        txtGreetingName,  114);

            this.grpLetter.Text = "Письмо";
            this.grpLetter.Location = new Point(4, 214);
            this.grpLetter.Size = new Size(648, 254);

            AddField(grpLetter, lblDate,         "Дата:",  txtDate,         24);
            AddField(grpLetter, lblLetterNum,    "Номер:", txtLetterNum,     54);
            AddField(grpLetter, lblLetterSubject,"Тема:",  txtLetterSubject, 84);

            this.lblLetterBody.Text = "Текст:";
            this.lblLetterBody.AutoSize = true;
            this.lblLetterBody.Location = new Point(10, 115);

            this.txtLetterBody.Location = new Point(155, 112);
            this.txtLetterBody.Size = new Size(483, 130);
            this.txtLetterBody.Multiline = true;
            this.txtLetterBody.ScrollBars = ScrollBars.Vertical;
            this.grpLetter.Controls.AddRange(new Control[] { lblLetterBody, txtLetterBody });

            this.grpSender.Text = "Отправитель";
            this.grpSender.Location = new Point(4, 474);
            this.grpSender.Size = new Size(648, 84);

            AddField(grpSender, lblSenderPost, "Должность:", txtSenderPost, 24);
            AddField(grpSender, lblSenderName, "ФИО:",       txtSenderName, 54);

            this.tabMain.Controls.AddRange(new Control[] {
                grpTemplate, grpRecipient, grpLetter, grpSender
            });

            this.lblAttList.Text = "Список приложений:";
            this.lblAttList.AutoSize = true;
            this.lblAttList.Location = new Point(8, 8);

            this.lstAttachments.Location = new Point(8, 26);
            this.lstAttachments.Size = new Size(490, 148);
            this.lstAttachments.SelectedIndexChanged += new EventHandler(this.lstAttachments_SelectedIndexChanged);

            this.btnAddAtt.Text = "Добавить";
            this.btnAddAtt.Location = new Point(508, 26);
            this.btnAddAtt.Size = new Size(130, 26);
            this.btnAddAtt.Click += new EventHandler(this.btnAddAtt_Click);

            this.btnRemoveAtt.Text = "Удалить";
            this.btnRemoveAtt.Location = new Point(508, 58);
            this.btnRemoveAtt.Size = new Size(130, 26);
            this.btnRemoveAtt.Click += new EventHandler(this.btnRemoveAtt_Click);

            this.btnAttUp.Text = "↑ Вверх";
            this.btnAttUp.Location = new Point(508, 90);
            this.btnAttUp.Size = new Size(130, 26);
            this.btnAttUp.Click += new EventHandler(this.btnAttUp_Click);

            this.btnAttDown.Text = "↓ Вниз";
            this.btnAttDown.Location = new Point(508, 122);
            this.btnAttDown.Size = new Size(130, 26);
            this.btnAttDown.Click += new EventHandler(this.btnAttDown_Click);

            this.grpAttEdit.Text = "Редактирование";
            this.grpAttEdit.Location = new Point(8, 184);
            this.grpAttEdit.Size = new Size(630, 360);

            this.lblAttTitle.Text = "Заголовок:";
            this.lblAttTitle.AutoSize = true;
            this.lblAttTitle.Location = new Point(10, 26);

            this.txtAttTitle.Location = new Point(155, 23);
            this.txtAttTitle.Width = 460;

            this.lblAttPages.Text = "Страниц:";
            this.lblAttPages.AutoSize = true;
            this.lblAttPages.Location = new Point(10, 58);

            this.numAttPages.Location = new Point(155, 55);
            this.numAttPages.Width = 60;
            this.numAttPages.Minimum = 1;
            this.numAttPages.Maximum = 999;
            this.numAttPages.Value = 1;

            this.lblAttText.Text = "Текст:";
            this.lblAttText.AutoSize = true;
            this.lblAttText.Location = new Point(10, 92);

            this.txtAttText.Location = new Point(155, 89);
            this.txtAttText.Size = new Size(460, 256);
            this.txtAttText.Multiline = true;
            this.txtAttText.ScrollBars = ScrollBars.Vertical;

            this.grpAttEdit.Controls.AddRange(new Control[] {
                lblAttTitle, txtAttTitle,
                lblAttPages, numAttPages,
                lblAttText,  txtAttText
            });

            this.tabAttachments.Controls.AddRange(new Control[] {
                lblAttList, lstAttachments,
                btnAddAtt, btnRemoveAtt, btnAttUp, btnAttDown,
                grpAttEdit
            });

            this.lblPreviewTitle.Text      = "Предпросмотр письма";
            this.lblPreviewTitle.Dock      = DockStyle.Top;
            this.lblPreviewTitle.Height    = 22;
            this.lblPreviewTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblPreviewTitle.Font      = new Font(this.Font, FontStyle.Bold);

            this.wbPreview.Dock = DockStyle.Fill;

            this.pnlPreview.Dock    = DockStyle.Right;
            this.pnlPreview.Width   = 420;
            this.pnlPreview.Padding = new Padding(0);

            this.pnlPreview.Controls.AddRange(new Control[] { this.wbPreview, this.lblPreviewTitle });

            this.Controls.AddRange(new Control[] { pnlBottom, pnlPreview, tabControl });

            this.ResumeLayout(false);
        }

        private static void AddField(Control parent, Label lbl, string labelText, TextBox txt, int y)
        {
            lbl.Text = labelText;
            lbl.AutoSize = true;
            lbl.Location = new Point(10, y + 3);
            txt.Location = new Point(155, y);
            txt.Width = 483;
            parent.Controls.AddRange(new Control[] { lbl, txt });
        }

        private TabControl  tabControl;
        private TabPage     tabMain;
        private TabPage     tabAttachments;

        private GroupBox    grpTemplate;
        private Label       lblTemplatePath;
        private TextBox     txtTemplatePath;
        private Button      btnBrowseTemplate;

        private GroupBox    grpRecipient;
        private Label       lblRecipientPost;
        private TextBox     txtRecipientPost;
        private Label       lblRecipientOrg;
        private TextBox     txtRecipientOrg;
        private Label       lblRecipientName;
        private TextBox     txtRecipientName;
        private Label       lblGreetingName;
        private TextBox     txtGreetingName;

        private GroupBox    grpLetter;
        private Label       lblDate;
        private TextBox     txtDate;
        private Label       lblLetterNum;
        private TextBox     txtLetterNum;
        private Label       lblLetterSubject;
        private TextBox     txtLetterSubject;
        private Label       lblLetterBody;
        private TextBox     txtLetterBody;

        private GroupBox    grpSender;
        private Label       lblSenderPost;
        private TextBox     txtSenderPost;
        private Label       lblSenderName;
        private TextBox     txtSenderName;

        private Label          lblAttList;
        private ListBox        lstAttachments;
        private Button         btnAddAtt;
        private Button         btnRemoveAtt;
        private Button         btnAttUp;
        private Button         btnAttDown;

        private GroupBox       grpAttEdit;
        private Label          lblAttTitle;
        private TextBox        txtAttTitle;
        private Label          lblAttPages;
        private NumericUpDown  numAttPages;
        private Label          lblAttText;
        private TextBox        txtAttText;

        private Panel   pnlBottom;
        private Button  btnFillDefaults;
        private Button  btnGenerate;
        private Label   lblStatus;

        private Panel       pnlPreview;
        private Label       lblPreviewTitle;
        private Microsoft.Web.WebView2.WinForms.WebView2  wbPreview;

    }
}
