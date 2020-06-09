using System;
using System.Windows.Forms;
using System.Drawing;
using PaintKiller;

public class OptionForm : Form
{
    private Button btnAccept;
    private Button btnOpen;
    private Button btnSave;
    private Button btnCleanup;
    private NumericUpDown udWidth;
    private ColorDialog cdColor;
    private PictureBox pbOutline;
    private PictureBox pbFill;
    private SaveFileDialog sdScene;
    private OpenFileDialog odScene;
    private void btnAcceptClick(object Sender, EventArgs e)
    {
        Form1.currentPen.Width = (float)udWidth.Value;
        Form1.currentPen.Color = pbOutline.BackColor;
        Form1.currentBrush.Color = pbFill.BackColor;
        Owner.Invalidate();

        Close();
    }
    private void btnCleanupClick(object Sender, EventArgs e)
    {
        Form1.scene.Empty();
    }
    private void btnOpenClick(object Sender, EventArgs e)
    {
        odScene.ShowDialog();
    }
    public void FileOpen(object Sender, EventArgs e)
    {
        Form1.scene.LoadFrom(odScene.FileName);
    }
    private void btnSaveClick(object Sender, EventArgs e)
    {
        sdScene.ShowDialog();
    }
    public void FileSave(object Sender, EventArgs e)
    {
        Form1.scene.SaveTo(sdScene.FileName);
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
        MaximizeBox = false;
        MinimizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        ShowInTaskbar = false;
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

        sdScene = new SaveFileDialog();
        sdScene.FileOk += new System.ComponentModel.CancelEventHandler(FileSave);
        
        btnSave = new Button();
        btnSave.Text = "Save";
        btnSave.Location = new Point(60, 60);
        btnSave.Size = new Size(80, 28);
        Controls.Add(btnSave);
        btnSave.Click += new EventHandler(btnSaveClick);

        odScene = new OpenFileDialog();
        odScene.FileOk += new System.ComponentModel.CancelEventHandler(FileOpen);
        
        btnOpen = new Button();
        btnOpen.Text = "Open";
        btnOpen.Location = new Point(60, 20);
        btnOpen.Size = new Size(80, 28);
        Controls.Add(btnOpen);
        btnOpen.Click += new EventHandler(btnOpenClick);
        
        btnCleanup = new Button();
        btnCleanup.Text = "Cleanup";
        btnCleanup.Location = new Point(60, 120);
        btnCleanup.Size = new Size(80, 28);
        Controls.Add(btnCleanup);
        btnCleanup.Click += new EventHandler(btnCleanupClick);

        btnAccept = new Button();
        btnAccept.Text = "Accept";
        btnAccept.Location = new Point(160, 180);
        btnAccept.Size = new Size(80, 28);
        Controls.Add(btnAccept);
        btnAccept.Click += new EventHandler(btnAcceptClick);
    }
}