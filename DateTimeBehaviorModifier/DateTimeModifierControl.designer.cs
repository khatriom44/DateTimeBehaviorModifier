namespace DateTimeBehaviorModifier
{
    partial class DateTimeModifierControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeModifierControl));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadTables = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.SelectCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SchemaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFormat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TargetBehavior = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleName = "";
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectCheckbox,
            this.SchemaName,
            this.colFormat,
            this.TargetBehavior});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.Location = new System.Drawing.Point(6, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.Size = new System.Drawing.Size(1887, 449);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadTables});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1896, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoadTables
            // 
            this.tsbLoadTables.AccessibleName = "Load Dataverse Tables";
            this.tsbLoadTables.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadTables.Image")));
            this.tsbLoadTables.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadTables.Name = "tsbLoadTables";
            this.tsbLoadTables.Size = new System.Drawing.Size(142, 22);
            this.tsbLoadTables.Text = "Load Dataverse Tables";
            this.tsbLoadTables.Click += new System.EventHandler(this.tsbLoadTables_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Table:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(79, 37);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(360, 21);
            this.cmbTables.TabIndex = 8;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.AutoEllipsis = true;
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.Location = new System.Drawing.Point(749, 519);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(426, 39);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update Field Behaviors";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // rtbLogs
            // 
            this.rtbLogs.Location = new System.Drawing.Point(6, 564);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(1887, 505);
            this.rtbLogs.TabIndex = 10;
            this.rtbLogs.Text = "";
            // 
            // SelectCheckbox
            // 
            this.SelectCheckbox.HeaderText = "Select";
            this.SelectCheckbox.Name = "SelectCheckbox";
            // 
            // SchemaName
            // 
            this.SchemaName.HeaderText = "Field Schema Name";
            this.SchemaName.Name = "SchemaName";
            this.SchemaName.ReadOnly = true;
            // 
            // colFormat
            // 
            this.colFormat.HeaderText = "Target Format";
            this.colFormat.Items.AddRange(new object[] {
            "DateOnly",
            "DateAndTime"});
            this.colFormat.Name = "colFormat";
            this.colFormat.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFormat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // TargetBehavior
            // 
            this.TargetBehavior.HeaderText = "Target Behavior";
            this.TargetBehavior.Items.AddRange(new object[] {
            "User local",
            "Date only",
            "Time zone independent"});
            this.TargetBehavior.Name = "TargetBehavior";
            // 
            // DateTimeModifierControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.rtbLogs);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DateTimeModifierControl";
            this.Size = new System.Drawing.Size(1896, 1108);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoadTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectCheckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemaName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colFormat;
        private System.Windows.Forms.DataGridViewComboBoxColumn TargetBehavior;
    }
}
