
namespace MccBrowser
{
    partial class Form1
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
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDecodedObjects = new System.Windows.Forms.Label();
            this.textBoxResultText = new System.Windows.Forms.TextBox();
            this.buttonBrowseMcc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInput.Location = new System.Drawing.Point(13, 36);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(965, 73);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.Text = "T59S594F7FZ0472F4FC97A2RFF0B33FE912AFEZ15FE4E65FE7874FE2074FE696DFE6520FE6F6EFE92" +
    "01FEZZM73F2E02020207E7FFFE1656E67C17FFF74Z04FEBB";
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(12, 115);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 94);
            this.buttonGo.TabIndex = 1;
            this.buttonGo.Text = "Decode";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(93, 144);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(885, 66);
            this.textBoxResult.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(13, 242);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(965, 366);
            this.treeView1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "MCC Vanc data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Decoded hex string";
            // 
            // labelDecodedObjects
            // 
            this.labelDecodedObjects.AutoSize = true;
            this.labelDecodedObjects.Location = new System.Drawing.Point(13, 221);
            this.labelDecodedObjects.Name = "labelDecodedObjects";
            this.labelDecodedObjects.Size = new System.Drawing.Size(95, 15);
            this.labelDecodedObjects.TabIndex = 6;
            this.labelDecodedObjects.Text = "Decoded objects";
            // 
            // textBoxResultText
            // 
            this.textBoxResultText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResultText.Location = new System.Drawing.Point(12, 615);
            this.textBoxResultText.Multiline = true;
            this.textBoxResultText.Name = "textBoxResultText";
            this.textBoxResultText.Size = new System.Drawing.Size(966, 64);
            this.textBoxResultText.TabIndex = 7;
            // 
            // buttonBrowseMcc
            // 
            this.buttonBrowseMcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseMcc.Location = new System.Drawing.Point(860, 7);
            this.buttonBrowseMcc.Name = "buttonBrowseMcc";
            this.buttonBrowseMcc.Size = new System.Drawing.Size(117, 23);
            this.buttonBrowseMcc.TabIndex = 8;
            this.buttonBrowseMcc.Text = "Browse mcc...";
            this.buttonBrowseMcc.UseVisualStyleBackColor = true;
            this.buttonBrowseMcc.Click += new System.EventHandler(this.buttonBrowseMcc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 691);
            this.Controls.Add(this.buttonBrowseMcc);
            this.Controls.Add(this.textBoxResultText);
            this.Controls.Add(this.labelDecodedObjects);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.textBoxInput);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "MCC vanc browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDecodedObjects;
        private System.Windows.Forms.TextBox textBoxResultText;
        private System.Windows.Forms.Button buttonBrowseMcc;
    }
}

