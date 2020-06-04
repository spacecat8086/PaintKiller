using System;
using System.Windows.Forms;
using System.Drawing;
using PaintKiller;

public class OptionForm : Form
{
    private Button btnClose;
    private NumericUpDown udWidth;
    private ColorDialog cdColor;
    private PictureBox pbOutline;
    private PictureBox pbFill;
    private ListBox lbShapeList;
    private void btnCloseClick(object Sender, EventArgs e)
    {
        Form1.currentPen.Width = (float)udWidth.Value;
        Form1.currentPen.Color = pbOutline.BackColor;
        Form1.currentBrush.Color = pbFill.BackColor;

        Close();
    }
    public void SelectColor(object Sender, EventArgs e)
    {
        cdColor.ShowDialog();
        (Sender as PictureBox).BackColor = cdColor.Color;
    }
    public OptionForm()
    {          
        DoubleBuffered = true;
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(400, 225);
        Text = "Options";

        udWidth = new NumericUpDown();
        udWidth.Location = new Point(220, 40);
        Controls.Add(udWidth);
        udWidth.Value = Convert.ToDecimal(Form1.currentPen.Width);

        pbOutline = new PictureBox();
        pbOutline.BackColor = Form1.currentPen.Color;
        pbOutline.Location = new Point(220, 80);
        pbOutline.Size = new Size(40, 40);
        pbOutline.Click += new EventHandler(SelectColor);
        Controls.Add(pbOutline);
        
        pbFill = new PictureBox();
        pbFill.BackColor = Form1.currentBrush.Color;
        pbFill.Location = new Point(280, 80);
        pbFill.Size = new Size(40, 40);
        pbFill.Click += new EventHandler(SelectColor);
        Controls.Add(pbFill);

        cdColor = new ColorDialog();
        cdColor.Color = Form1.currentPen.Color;

        lbShapeList = new ListBox();
        lbShapeList.Location = new Point(40, 40);
        lbShapeList.DataSource = Form1.scene.shapes;
        //lbShapeList.DisplayMember

        btnClose = new Button();
        btnClose.Text = "Close";
        btnClose.Location = new Point(160, 180);
        btnClose.Size = new Size(80, 28);
        Controls.Add(btnClose);
        btnClose.Click += new EventHandler(btnCloseClick);
    }
}