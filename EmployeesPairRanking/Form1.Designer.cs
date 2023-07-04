namespace EmployeesPairRanking
{
    partial class frmCommonProjetcs
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dlgSelectFile = new OpenFileDialog();
            btnSelectFile = new Button();
            lblSelectedFile = new Label();
            dgEmployyesPairRanking = new DataGridView();
            lblCommonProjects = new Label();
            lblResult = new Label();
            ((System.ComponentModel.ISupportInitialize)dgEmployyesPairRanking).BeginInit();
            SuspendLayout();

            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(16, 11);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(75, 23);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "Select File";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // lblSelectedFile
            // 
            lblSelectedFile.AutoSize = true;
            lblSelectedFile.Location = new Point(116, 19);
            lblSelectedFile.Name = "lblSelectedFile";
            lblSelectedFile.Size = new Size(75, 15);
            lblSelectedFile.TabIndex = 1;
            lblSelectedFile.Text = "Selected File:";
            lblSelectedFile.Visible = false;
            // 
            // dgEmployyesPairRanking
            // 
            dgEmployyesPairRanking.AllowUserToAddRows = false;
            dgEmployyesPairRanking.AllowUserToDeleteRows = false;
            dgEmployyesPairRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgEmployyesPairRanking.Location = new Point(16, 144);
            dgEmployyesPairRanking.Name = "dgEmployyesPairRanking";
            dgEmployyesPairRanking.ReadOnly = true;
            dgEmployyesPairRanking.RowTemplate.Height = 25;
            dgEmployyesPairRanking.Size = new Size(486, 150);
            dgEmployyesPairRanking.TabIndex = 2;
            dgEmployyesPairRanking.Visible = false;
            // 
            // lblCommonProjects
            // 
            lblCommonProjects.AutoSize = true;
            lblCommonProjects.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCommonProjects.Location = new Point(16, 114);
            lblCommonProjects.Name = "lblCommonProjects";
            lblCommonProjects.Size = new Size(105, 15);
            lblCommonProjects.TabIndex = 3;
            lblCommonProjects.Text = "Common Projects";
            lblCommonProjects.Visible = false;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblResult.Location = new Point(16, 66);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(0, 15);
            lblResult.TabIndex = 4;
            lblResult.Visible = false;
            // 
            // frmCommonProjetcs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblResult);
            Controls.Add(lblCommonProjects);
            Controls.Add(dgEmployyesPairRanking);
            Controls.Add(lblSelectedFile);
            Controls.Add(btnSelectFile);
            Name = "frmCommonProjetcs";
            Text = "Common Projetcs";
            ((System.ComponentModel.ISupportInitialize)dgEmployyesPairRanking).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog dlgSelectFile;
        private Button btnSelectFile;
        private Label lblSelectedFile;
        private DataGridView dgEmployyesPairRanking;
        private Label lblCommonProjects;
        private Label lblResult;
    }
}