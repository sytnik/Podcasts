using System.ComponentModel;

namespace ArraySortCore
{
    public partial class FormMain
    {
        private IContainer components = (IContainer)null;
        private Button buttonSort;
        private GroupBox sortingType;
        private RadioButton Bubble;
        private RadioButton Quick;
        private RadioButton Selection;
        private RadioButton Insertion;
        private NumericUpDown sizeOfArray;
        private Label labelSizeOfArray;
        private ProgressBar pBar;
        private TextBox textBoxResult;
        private RadioButton Shell;
        private RadioButton Merge;
        private RadioButton Gnome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            buttonSort = new Button();
            sortingType = new GroupBox();
            Gnome = new RadioButton();
            Merge = new RadioButton();
            Shell = new RadioButton();
            Quick = new RadioButton();
            Selection = new RadioButton();
            Insertion = new RadioButton();
            Bubble = new RadioButton();
            sizeOfArray = new NumericUpDown();
            labelSizeOfArray = new Label();
            pBar = new ProgressBar();
            textBoxResult = new TextBox();
            btnBatch = new Button();
            sortingType.SuspendLayout();
            ((ISupportInitialize)sizeOfArray).BeginInit();
            SuspendLayout();
            // 
            // buttonSort
            // 
            buttonSort.Anchor = AnchorStyles.Top;
            buttonSort.Location = new Point(54, 447);
            buttonSort.Margin = new Padding(5, 6, 5, 6);
            buttonSort.Name = "buttonSort";
            buttonSort.Size = new Size(125, 44);
            buttonSort.TabIndex = 0;
            buttonSort.Text = "Сортировка";
            buttonSort.UseVisualStyleBackColor = true;
            // 
            // sortingType
            // 
            sortingType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sortingType.Controls.Add(Gnome);
            sortingType.Controls.Add(Merge);
            sortingType.Controls.Add(Shell);
            sortingType.Controls.Add(Quick);
            sortingType.Controls.Add(Selection);
            sortingType.Controls.Add(Insertion);
            sortingType.Controls.Add(Bubble);
            sortingType.Location = new Point(21, 25);
            sortingType.Margin = new Padding(5, 6, 5, 6);
            sortingType.Name = "sortingType";
            sortingType.Padding = new Padding(5, 6, 5, 6);
            sortingType.Size = new Size(503, 352);
            sortingType.TabIndex = 1;
            sortingType.TabStop = false;
            sortingType.Text = "Тип сортировки";
            // 
            // Gnome
            // 
            Gnome.AutoSize = true;
            Gnome.Location = new Point(8, 171);
            Gnome.Margin = new Padding(5, 6, 5, 6);
            Gnome.Name = "Gnome";
            Gnome.Size = new Size(97, 29);
            Gnome.TabIndex = 6;
            Gnome.TabStop = true;
            Gnome.Text = "Гномья";
            Gnome.UseVisualStyleBackColor = true;
            // 
            // Merge
            // 
            Merge.AutoSize = true;
            Merge.Location = new Point(10, 307);
            Merge.Margin = new Padding(5, 6, 5, 6);
            Merge.Name = "Merge";
            Merge.Size = new Size(118, 29);
            Merge.TabIndex = 5;
            Merge.TabStop = true;
            Merge.Text = "Слиянием";
            Merge.UseVisualStyleBackColor = true;
            // 
            // Shell
            // 
            Shell.AutoSize = true;
            Shell.Location = new Point(8, 261);
            Shell.Margin = new Padding(5, 6, 5, 6);
            Shell.Name = "Shell";
            Shell.Size = new Size(90, 29);
            Shell.TabIndex = 4;
            Shell.TabStop = true;
            Shell.Text = "Шелла";
            Shell.UseVisualStyleBackColor = true;
            // 
            // Quick
            // 
            Quick.AutoSize = true;
            Quick.Location = new Point(10, 216);
            Quick.Margin = new Padding(5, 6, 5, 6);
            Quick.Name = "Quick";
            Quick.Size = new Size(104, 29);
            Quick.TabIndex = 3;
            Quick.Text = "Быстрая";
            Quick.UseVisualStyleBackColor = true;
            // 
            // Selection
            // 
            Selection.AutoSize = true;
            Selection.Location = new Point(10, 129);
            Selection.Margin = new Padding(5, 6, 5, 6);
            Selection.Name = "Selection";
            Selection.Size = new Size(116, 29);
            Selection.TabIndex = 2;
            Selection.Text = "Выбором";
            Selection.UseVisualStyleBackColor = true;
            // 
            // Insertion
            // 
            Insertion.AutoSize = true;
            Insertion.Location = new Point(10, 82);
            Insertion.Margin = new Padding(5, 6, 5, 6);
            Insertion.Name = "Insertion";
            Insertion.Size = new Size(122, 29);
            Insertion.TabIndex = 1;
            Insertion.Text = "Вставками";
            Insertion.UseVisualStyleBackColor = true;
            // 
            // Bubble
            // 
            Bubble.AutoSize = true;
            Bubble.Checked = true;
            Bubble.Location = new Point(10, 36);
            Bubble.Margin = new Padding(5, 6, 5, 6);
            Bubble.Name = "Bubble";
            Bubble.Size = new Size(148, 29);
            Bubble.TabIndex = 0;
            Bubble.TabStop = true;
            Bubble.Text = "Пузырьковая";
            Bubble.UseVisualStyleBackColor = true;
            // 
            // sizeOfArray
            // 
            sizeOfArray.Anchor = AnchorStyles.Top;
            sizeOfArray.Location = new Point(237, 389);
            sizeOfArray.Margin = new Padding(5, 6, 5, 6);
            sizeOfArray.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            sizeOfArray.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            sizeOfArray.Name = "sizeOfArray";
            sizeOfArray.Size = new Size(287, 31);
            sizeOfArray.TabIndex = 2;
            sizeOfArray.Value = new decimal(new int[] { 30000, 0, 0, 0 });
            // 
            // labelSizeOfArray
            // 
            labelSizeOfArray.Anchor = AnchorStyles.Top;
            labelSizeOfArray.AutoSize = true;
            labelSizeOfArray.Location = new Point(20, 393);
            labelSizeOfArray.Margin = new Padding(5, 0, 5, 0);
            labelSizeOfArray.Name = "labelSizeOfArray";
            labelSizeOfArray.Size = new Size(198, 25);
            labelSizeOfArray.TabIndex = 3;
            labelSizeOfArray.Text = "Количество элементов";
            // 
            // pBar
            // 
            pBar.Location = new Point(14, 521);
            pBar.Margin = new Padding(5, 6, 5, 6);
            pBar.Name = "pBar";
            pBar.Size = new Size(503, 44);
            pBar.Step = 1;
            pBar.TabIndex = 4;
            // 
            // textBoxResult
            // 
            textBoxResult.Dock = DockStyle.Bottom;
            textBoxResult.Location = new Point(0, 577);
            textBoxResult.Margin = new Padding(5, 6, 5, 6);
            textBoxResult.Multiline = true;
            textBoxResult.Name = "textBoxResult";
            textBoxResult.Size = new Size(540, 648);
            textBoxResult.TabIndex = 5;
            // 
            // btnBatch
            // 
            btnBatch.Anchor = AnchorStyles.Top;
            btnBatch.Location = new Point(257, 447);
            btnBatch.Name = "btnBatch";
            btnBatch.Size = new Size(163, 44);
            btnBatch.TabIndex = 6;
            btnBatch.Text = "Batch run";
            btnBatch.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 1225);
            Controls.Add(btnBatch);
            Controls.Add(textBoxResult);
            Controls.Add(pBar);
            Controls.Add(labelSizeOfArray);
            Controls.Add(sizeOfArray);
            Controls.Add(sortingType);
            Controls.Add(buttonSort);
            Margin = new Padding(5, 6, 5, 6);
            MinimumSize = new Size(546, 420);
            Name = "FormMain";
            Text = "Sorting comparison";
            sortingType.ResumeLayout(false);
            sortingType.PerformLayout();
            ((ISupportInitialize)sizeOfArray).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnBatch;
    }
}
