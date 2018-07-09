namespace CBS_Project
{
     partial class InfoForm
     {
          /// <summary>
          /// Required designer variable.
          /// </summary>
          private System.ComponentModel.IContainer components = null;

          /// <summary>
          /// Clean up any resources being used.
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
          /// Required method for Designer support - do not modify
          /// the contents of this method with the code editor.
          /// </summary>
          private void InitializeComponent()
          {
               System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
               this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
               this.comboBox1 = new System.Windows.Forms.ComboBox();
               this.button1 = new System.Windows.Forms.Button();
               this.comboBox2 = new System.Windows.Forms.ComboBox();
               this.label2 = new System.Windows.Forms.Label();
               this.label1 = new System.Windows.Forms.Label();
               this.label4 = new System.Windows.Forms.Label();
               ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
               this.SuspendLayout();
               // 
               // chart1
               // 
               chartArea7.Name = "ChartArea1";
               this.chart1.ChartAreas.Add(chartArea7);
               this.chart1.Location = new System.Drawing.Point(29, 243);
               this.chart1.Name = "chart1";
               this.chart1.Size = new System.Drawing.Size(586, 297);
               this.chart1.TabIndex = 3;
               this.chart1.Text = "chart1";
               // 
               // comboBox1
               // 
               this.comboBox1.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.comboBox1.FormattingEnabled = true;
               this.comboBox1.Items.AddRange(new object[] {
            "Issizlik",
            "Egitim",
            "Goc"});
               this.comboBox1.Location = new System.Drawing.Point(460, 140);
               this.comboBox1.Name = "comboBox1";
               this.comboBox1.Size = new System.Drawing.Size(121, 28);
               this.comboBox1.TabIndex = 4;
               this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
               // 
               // button1
               // 
               this.button1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.button1.Location = new System.Drawing.Point(243, 110);
               this.button1.Name = "button1";
               this.button1.Size = new System.Drawing.Size(147, 72);
               this.button1.TabIndex = 6;
               this.button1.Text = "Show";
               this.button1.UseVisualStyleBackColor = true;
               this.button1.Click += new System.EventHandler(this.button1_Click);
               // 
               // comboBox2
               // 
               this.comboBox2.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.comboBox2.FormattingEnabled = true;
               this.comboBox2.Items.AddRange(new object[] {
            "2010",
            "2011",
            "2012",
            "2013"});
               this.comboBox2.Location = new System.Drawing.Point(46, 140);
               this.comboBox2.Name = "comboBox2";
               this.comboBox2.Size = new System.Drawing.Size(121, 28);
               this.comboBox2.TabIndex = 7;
               this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.BackColor = System.Drawing.Color.Transparent;
               this.label2.Font = new System.Drawing.Font("Cambria", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label2.ForeColor = System.Drawing.Color.White;
               this.label2.Location = new System.Drawing.Point(455, 110);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(65, 27);
               this.label2.TabIndex = 9;
               this.label2.Text = "Table";
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.BackColor = System.Drawing.Color.Transparent;
               this.label1.Font = new System.Drawing.Font("Cambria", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
               this.label1.Location = new System.Drawing.Point(41, 110);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(55, 27);
               this.label1.TabIndex = 10;
               this.label1.Text = "Year";
               // 
               // label4
               // 
               this.label4.AutoSize = true;
               this.label4.BackColor = System.Drawing.Color.Transparent;
               this.label4.Font = new System.Drawing.Font("Cambria", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label4.ForeColor = System.Drawing.Color.White;
               this.label4.Location = new System.Drawing.Point(170, 195);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(65, 27);
               this.label4.TabIndex = 12;
               this.label4.Text = "Table";
               this.label4.Visible = false;
               // 
               // InfoForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(638, 561);
               this.Controls.Add(this.label4);
               this.Controls.Add(this.label1);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.comboBox2);
               this.Controls.Add(this.button1);
               this.Controls.Add(this.comboBox1);
               this.Controls.Add(this.chart1);
               this.Name = "InfoForm";
               this.Text = "InfoForm";
               this.Load += new System.EventHandler(this.InfoForm_Load);
               ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion
          private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
          private System.Windows.Forms.ComboBox comboBox1;
          private System.Windows.Forms.Button button1;
          private System.Windows.Forms.ComboBox comboBox2;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.Label label1;
          private System.Windows.Forms.Label label4;
     }
}