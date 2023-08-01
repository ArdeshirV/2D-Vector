#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT

using System;
using System.Web;
using System.IO;
//using ArdeshirV.Utility;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion
//-----------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// About box form in vector project.
	/// </summary>
	public class frmAbout : 
		System.Windows.Forms.Form
	{
		#region Variables

		private bool m_blnIsShrinked = false;
		private static bool s_blnIsExists = false;
		private readonly string m_strSystemInfo =
        	Environment.SystemDirectory + "\\msinfo32.exe";
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Button btnMore;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.LinkLabel lnkLink;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Button btnSysteminfo;
		private System.Windows.Forms.Label lblApplicationName;
		private System.Windows.Forms.PictureBox picApplicationIcon;
		private System.ComponentModel.IContainer components = null;

		#endregion
		//---------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public frmAbout()
		{
			InitializeComponent();

			StartPosition = FormStartPosition.CenterParent;
			this.btnSysteminfo.Enabled = System.IO.File.Exists(m_strSystemInfo);
		}
		#endregion
		//---------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Test exist an intance of this form.
		/// </summary>
		public static bool Exists
		{
			get
			{
				return s_blnIsExists;
			}
		}

		#endregion
		//---------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occurs whenever OK Button became Click.
		/// </summary>
		/// <param name="sender">OK button</param>
		/// <param name="e">Event argument</param>
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			//DialogResult = DialogResult.OK;
			Close();
		}

		/// <summary>
		/// occurs whenever system info button has been clecked.
		/// </summary>
		/// <param name="sender">System Info... button</param>
		/// <param name="e">Event argument</param>
		private void btnSysteminfo_Click(object sender, System.EventArgs e)
		{
			//ArdeshirV.Utility.SystemInfo.Show();
			//ArdeshirV.Utility.GlabalUtility.ShowSysInfo();
            string l_strSystemInfo = Environment.SystemDirectory + "\\msinfo32.exe";

            if (File.Exists(l_strSystemInfo))
                Process.Start(l_strSystemInfo);
            else
                throw new FileNotFoundException(string.Format(
            		"File {0} missing!", l_strSystemInfo));
		}

		/// <summary>
		/// Launch to favorite web site.
		/// </summary>
		/// <param name="sender">Link label</param>
		/// <param name="e">Event argument</param>
        private void lnkLink_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
		{
			lnkLink.LinkVisited = true;
			System.Diagnostics.Process.Start("https://github.com/ArdeshirV/2D-Vector");
		}

		/// <summary>
		/// Occurs whenever Dialog has been loaded.
		/// </summary>
		/// <param name="sender">Form about</param>
		/// <param name="e">Event argument</param>
		private void frmAbout_Load(object sender, System.EventArgs e)
		{
#if !DEBUG
			//OpacityChangerInterval = 60;
#endif
			if(s_blnIsExists)
				Close();
			else
				s_blnIsExists = true;
		}

		/// <summary>
		/// Occured whenever btnMore button has been clicked.
		/// </summary>
		/// <param name="sender">More button</param>
		/// <param name="e">Event argument</param>
		private void btnMore_Click(object sender, System.EventArgs e)
		{
			m_blnIsShrinked = !m_blnIsShrinked;
			if(Height <= 200)
			{
				btnMore.Text = "&Less <<";
				Top = Top - 134;
				Height = 412;
			}
			else
			{
				StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				btnMore.Text = "&More >>";
				Top = Top + 134;
				Height = 163;
			}
		}

		/// <summary>
		/// Occured whenever btnSend button has been clicked.
		/// </summary>
		/// <param name="sender">Send button</param>
		/// <param name="e">Event argument</param>
		private void btnSend_Click(object sender, System.EventArgs e)
		{
			System.Web.Mail.MailMessage l_milMessage = new System.Web.Mail.MailMessage();
			string l_strMessage;

			l_strMessage =
				"User name: "  + Environment.UserName + "\n" +
				"Current date & time: " + System.DateTime.Now.ToString() +
				Environment.NewLine + "Machine Name: " + Environment.MachineName +
				", Username: " + Environment.UserName + Environment.NewLine +
				"-------------------------------Contents------------------------\n" +
				txtMessage.Text + "\n" +
				"---------------------------------------------------------------\n";
			l_milMessage.From = "nobody@somewhere.net";
			l_milMessage.To = "ArdeshirV@protonmail.com";
			l_milMessage.Subject = "[Robot]Automatic Sent from vector user.";
			l_milMessage.Body = l_strMessage;
			l_milMessage.BodyFormat = System.Web.Mail.MailFormat.Text;

			try
			{
				System.Web.Mail.SmtpMail.Send(l_milMessage);
			}
			catch(Exception exp)
			{
				MessageBox.Show(this, exp.Message, "Error!", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			((Button)sender).Enabled = false;
		}

		/// <summary>
		/// Occured wenever txtMessage has been changed.
		/// </summary>
		/// <param name="sender">Message text box</param>
		/// <param name="e">Event argument</param>
		private void txtMessage_TextChanged(object sender, System.EventArgs e)
		{
			btnSend.Enabled = true;
		}

		/// <summary>
		/// Occured whenever form has been closed.
		/// </summary>
		/// <param name="sender">About form</param>
		/// <param name="e">Event argument</param>
		private void frmAbout_Closed(object sender, System.EventArgs e)
		{
			s_blnIsExists = false;
		}

		#endregion
		//---------------------------------------------------------------------
		#region overrided functions

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
				if(components != null)
					components.Dispose();

			base.Dispose(disposing);
		}

		#endregion
		//---------------------------------------------------------------------
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.picApplicationIcon = new System.Windows.Forms.PictureBox();
			this.lblApplicationName = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnSysteminfo = new System.Windows.Forms.Button();
			this.lnkLink = new System.Windows.Forms.LinkLabel();
			this.btnMore = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.picApplicationIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// lblCopyright
			// 
			this.lblCopyright.AccessibleDescription = resources.GetString("lblCopyright.AccessibleDescription");
			this.lblCopyright.AccessibleName = resources.GetString("lblCopyright.AccessibleName");
			this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCopyright.Anchor")));
			this.lblCopyright.AutoSize = ((bool)(resources.GetObject("lblCopyright.AutoSize")));
			this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
			this.lblCopyright.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("lblCopyright.BackgroundImageLayout")));
			this.lblCopyright.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCopyright.Dock")));
			this.lblCopyright.Font = ((System.Drawing.Font)(resources.GetObject("lblCopyright.Font")));
			this.lblCopyright.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCopyright.ImageAlign")));
			this.lblCopyright.ImageIndex = ((int)(resources.GetObject("lblCopyright.ImageIndex")));
			this.lblCopyright.ImageKey = resources.GetString("lblCopyright.ImageKey");
			this.lblCopyright.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCopyright.ImeMode")));
			this.lblCopyright.Location = ((System.Drawing.Point)(resources.GetObject("lblCopyright.Location")));
			this.lblCopyright.MaximumSize = ((System.Drawing.Size)(resources.GetObject("lblCopyright.MaximumSize")));
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCopyright.RightToLeft")));
			this.lblCopyright.Size = ((System.Drawing.Size)(resources.GetObject("lblCopyright.Size")));
			this.lblCopyright.TabIndex = ((int)(resources.GetObject("lblCopyright.TabIndex")));
			this.lblCopyright.Text = resources.GetString("lblCopyright.Text");
			this.lblCopyright.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCopyright.TextAlign")));
			// 
			// lblVersion
			// 
			this.lblVersion.AccessibleDescription = resources.GetString("lblVersion.AccessibleDescription");
			this.lblVersion.AccessibleName = resources.GetString("lblVersion.AccessibleName");
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVersion.Anchor")));
			this.lblVersion.AutoSize = ((bool)(resources.GetObject("lblVersion.AutoSize")));
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("lblVersion.BackgroundImageLayout")));
			this.lblVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVersion.Dock")));
			this.lblVersion.Font = ((System.Drawing.Font)(resources.GetObject("lblVersion.Font")));
			this.lblVersion.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVersion.ImageAlign")));
			this.lblVersion.ImageIndex = ((int)(resources.GetObject("lblVersion.ImageIndex")));
			this.lblVersion.ImageKey = resources.GetString("lblVersion.ImageKey");
			this.lblVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVersion.ImeMode")));
			this.lblVersion.Location = ((System.Drawing.Point)(resources.GetObject("lblVersion.Location")));
			this.lblVersion.MaximumSize = ((System.Drawing.Size)(resources.GetObject("lblVersion.MaximumSize")));
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVersion.RightToLeft")));
			this.lblVersion.Size = ((System.Drawing.Size)(resources.GetObject("lblVersion.Size")));
			this.lblVersion.TabIndex = ((int)(resources.GetObject("lblVersion.TabIndex")));
			this.lblVersion.Text = resources.GetString("lblVersion.Text");
			this.lblVersion.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVersion.TextAlign")));
			// 
			// picApplicationIcon
			// 
			this.picApplicationIcon.AccessibleDescription = resources.GetString("picApplicationIcon.AccessibleDescription");
			this.picApplicationIcon.AccessibleName = resources.GetString("picApplicationIcon.AccessibleName");
			this.picApplicationIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("picApplicationIcon.Anchor")));
			this.picApplicationIcon.BackColor = System.Drawing.Color.White;
			this.picApplicationIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picApplicationIcon.BackgroundImage")));
			this.picApplicationIcon.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("picApplicationIcon.BackgroundImageLayout")));
			this.picApplicationIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picApplicationIcon.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("picApplicationIcon.Dock")));
			this.picApplicationIcon.Font = ((System.Drawing.Font)(resources.GetObject("picApplicationIcon.Font")));
			this.picApplicationIcon.Image = ((System.Drawing.Image)(resources.GetObject("picApplicationIcon.Image")));
			this.picApplicationIcon.ImageLocation = resources.GetString("picApplicationIcon.ImageLocation");
			this.picApplicationIcon.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("picApplicationIcon.ImeMode")));
			this.picApplicationIcon.Location = ((System.Drawing.Point)(resources.GetObject("picApplicationIcon.Location")));
			this.picApplicationIcon.MaximumSize = ((System.Drawing.Size)(resources.GetObject("picApplicationIcon.MaximumSize")));
			this.picApplicationIcon.Name = "picApplicationIcon";
			this.picApplicationIcon.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("picApplicationIcon.RightToLeft")));
			this.picApplicationIcon.Size = ((System.Drawing.Size)(resources.GetObject("picApplicationIcon.Size")));
			this.picApplicationIcon.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("picApplicationIcon.SizeMode")));
			this.picApplicationIcon.TabIndex = ((int)(resources.GetObject("picApplicationIcon.TabIndex")));
			this.picApplicationIcon.TabStop = false;
			this.picApplicationIcon.WaitOnLoad = ((bool)(resources.GetObject("picApplicationIcon.WaitOnLoad")));
			// 
			// lblApplicationName
			// 
			this.lblApplicationName.AccessibleDescription = resources.GetString("lblApplicationName.AccessibleDescription");
			this.lblApplicationName.AccessibleName = resources.GetString("lblApplicationName.AccessibleName");
			this.lblApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblApplicationName.Anchor")));
			this.lblApplicationName.AutoSize = ((bool)(resources.GetObject("lblApplicationName.AutoSize")));
			this.lblApplicationName.BackColor = System.Drawing.Color.Transparent;
			this.lblApplicationName.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("lblApplicationName.BackgroundImageLayout")));
			this.lblApplicationName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblApplicationName.Dock")));
			this.lblApplicationName.Font = ((System.Drawing.Font)(resources.GetObject("lblApplicationName.Font")));
			this.lblApplicationName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblApplicationName.ImageAlign")));
			this.lblApplicationName.ImageIndex = ((int)(resources.GetObject("lblApplicationName.ImageIndex")));
			this.lblApplicationName.ImageKey = resources.GetString("lblApplicationName.ImageKey");
			this.lblApplicationName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblApplicationName.ImeMode")));
			this.lblApplicationName.Location = ((System.Drawing.Point)(resources.GetObject("lblApplicationName.Location")));
			this.lblApplicationName.MaximumSize = ((System.Drawing.Size)(resources.GetObject("lblApplicationName.MaximumSize")));
			this.lblApplicationName.Name = "lblApplicationName";
			this.lblApplicationName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblApplicationName.RightToLeft")));
			this.lblApplicationName.Size = ((System.Drawing.Size)(resources.GetObject("lblApplicationName.Size")));
			this.lblApplicationName.TabIndex = ((int)(resources.GetObject("lblApplicationName.TabIndex")));
			this.lblApplicationName.Text = resources.GetString("lblApplicationName.Text");
			this.lblApplicationName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblApplicationName.TextAlign")));
			// 
			// btnOk
			// 
			this.btnOk.AccessibleDescription = resources.GetString("btnOk.AccessibleDescription");
			this.btnOk.AccessibleName = resources.GetString("btnOk.AccessibleName");
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOk.Anchor")));
			this.btnOk.AutoSize = ((bool)(resources.GetObject("btnOk.AutoSize")));
			this.btnOk.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("btnOk.AutoSizeMode")));
			this.btnOk.BackColor = System.Drawing.Color.Transparent;
			this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
			this.btnOk.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("btnOk.BackgroundImageLayout")));
			this.btnOk.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOk.Dock")));
			this.btnOk.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOk.FlatStyle")));
			this.btnOk.Font = ((System.Drawing.Font)(resources.GetObject("btnOk.Font")));
			this.btnOk.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOk.ImageAlign")));
			this.btnOk.ImageIndex = ((int)(resources.GetObject("btnOk.ImageIndex")));
			this.btnOk.ImageKey = resources.GetString("btnOk.ImageKey");
			this.btnOk.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOk.ImeMode")));
			this.btnOk.Location = ((System.Drawing.Point)(resources.GetObject("btnOk.Location")));
			this.btnOk.MaximumSize = ((System.Drawing.Size)(resources.GetObject("btnOk.MaximumSize")));
			this.btnOk.Name = "btnOk";
			this.btnOk.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOk.RightToLeft")));
			this.btnOk.Size = ((System.Drawing.Size)(resources.GetObject("btnOk.Size")));
			this.btnOk.TabIndex = ((int)(resources.GetObject("btnOk.TabIndex")));
			this.btnOk.Text = resources.GetString("btnOk.Text");
			this.btnOk.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOk.TextAlign")));
			this.btnOk.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("btnOk.TextImageRelation")));
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnSysteminfo
			// 
			this.btnSysteminfo.AccessibleDescription = resources.GetString("btnSysteminfo.AccessibleDescription");
			this.btnSysteminfo.AccessibleName = resources.GetString("btnSysteminfo.AccessibleName");
			this.btnSysteminfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSysteminfo.Anchor")));
			this.btnSysteminfo.AutoSize = ((bool)(resources.GetObject("btnSysteminfo.AutoSize")));
			this.btnSysteminfo.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("btnSysteminfo.AutoSizeMode")));
			this.btnSysteminfo.BackColor = System.Drawing.Color.Transparent;
			this.btnSysteminfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSysteminfo.BackgroundImage")));
			this.btnSysteminfo.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("btnSysteminfo.BackgroundImageLayout")));
			this.btnSysteminfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSysteminfo.Dock")));
			this.btnSysteminfo.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSysteminfo.FlatStyle")));
			this.btnSysteminfo.Font = ((System.Drawing.Font)(resources.GetObject("btnSysteminfo.Font")));
			this.btnSysteminfo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSysteminfo.ImageAlign")));
			this.btnSysteminfo.ImageIndex = ((int)(resources.GetObject("btnSysteminfo.ImageIndex")));
			this.btnSysteminfo.ImageKey = resources.GetString("btnSysteminfo.ImageKey");
			this.btnSysteminfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSysteminfo.ImeMode")));
			this.btnSysteminfo.Location = ((System.Drawing.Point)(resources.GetObject("btnSysteminfo.Location")));
			this.btnSysteminfo.MaximumSize = ((System.Drawing.Size)(resources.GetObject("btnSysteminfo.MaximumSize")));
			this.btnSysteminfo.Name = "btnSysteminfo";
			this.btnSysteminfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSysteminfo.RightToLeft")));
			this.btnSysteminfo.Size = ((System.Drawing.Size)(resources.GetObject("btnSysteminfo.Size")));
			this.btnSysteminfo.TabIndex = ((int)(resources.GetObject("btnSysteminfo.TabIndex")));
			this.btnSysteminfo.Text = resources.GetString("btnSysteminfo.Text");
			this.btnSysteminfo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSysteminfo.TextAlign")));
			this.btnSysteminfo.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("btnSysteminfo.TextImageRelation")));
			this.btnSysteminfo.UseVisualStyleBackColor = false;
			this.btnSysteminfo.Click += new System.EventHandler(this.btnSysteminfo_Click);
			// 
			// lnkLink
			// 
			this.lnkLink.AccessibleDescription = resources.GetString("lnkLink.AccessibleDescription");
			this.lnkLink.AccessibleName = resources.GetString("lnkLink.AccessibleName");
			this.lnkLink.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lnkLink.Anchor")));
			this.lnkLink.AutoSize = ((bool)(resources.GetObject("lnkLink.AutoSize")));
			this.lnkLink.BackColor = System.Drawing.Color.Transparent;
			this.lnkLink.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("lnkLink.BackgroundImageLayout")));
			this.lnkLink.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lnkLink.Dock")));
			this.lnkLink.Font = ((System.Drawing.Font)(resources.GetObject("lnkLink.Font")));
			this.lnkLink.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lnkLink.ImageAlign")));
			this.lnkLink.ImageIndex = ((int)(resources.GetObject("lnkLink.ImageIndex")));
			this.lnkLink.ImageKey = resources.GetString("lnkLink.ImageKey");
			this.lnkLink.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lnkLink.ImeMode")));
			this.lnkLink.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lnkLink.LinkArea")));
			this.lnkLink.Location = ((System.Drawing.Point)(resources.GetObject("lnkLink.Location")));
			this.lnkLink.MaximumSize = ((System.Drawing.Size)(resources.GetObject("lnkLink.MaximumSize")));
			this.lnkLink.Name = "lnkLink";
			this.lnkLink.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lnkLink.RightToLeft")));
			this.lnkLink.Size = ((System.Drawing.Size)(resources.GetObject("lnkLink.Size")));
			this.lnkLink.TabIndex = ((int)(resources.GetObject("lnkLink.TabIndex")));
			this.lnkLink.TabStop = true;
			this.lnkLink.Text = resources.GetString("lnkLink.Text");
			this.lnkLink.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lnkLink.TextAlign")));
			this.lnkLink.UseCompatibleTextRendering = true;
			this.lnkLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLink_LinkClicked);
			// 
			// btnMore
			// 
			this.btnMore.AccessibleDescription = resources.GetString("btnMore.AccessibleDescription");
			this.btnMore.AccessibleName = resources.GetString("btnMore.AccessibleName");
			this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMore.Anchor")));
			this.btnMore.AutoSize = ((bool)(resources.GetObject("btnMore.AutoSize")));
			this.btnMore.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("btnMore.AutoSizeMode")));
			this.btnMore.BackColor = System.Drawing.Color.Transparent;
			this.btnMore.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMore.BackgroundImage")));
			this.btnMore.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("btnMore.BackgroundImageLayout")));
			this.btnMore.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMore.Dock")));
			this.btnMore.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMore.FlatStyle")));
			this.btnMore.Font = ((System.Drawing.Font)(resources.GetObject("btnMore.Font")));
			this.btnMore.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMore.ImageAlign")));
			this.btnMore.ImageIndex = ((int)(resources.GetObject("btnMore.ImageIndex")));
			this.btnMore.ImageKey = resources.GetString("btnMore.ImageKey");
			this.btnMore.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMore.ImeMode")));
			this.btnMore.Location = ((System.Drawing.Point)(resources.GetObject("btnMore.Location")));
			this.btnMore.MaximumSize = ((System.Drawing.Size)(resources.GetObject("btnMore.MaximumSize")));
			this.btnMore.Name = "btnMore";
			this.btnMore.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMore.RightToLeft")));
			this.btnMore.Size = ((System.Drawing.Size)(resources.GetObject("btnMore.Size")));
			this.btnMore.TabIndex = ((int)(resources.GetObject("btnMore.TabIndex")));
			this.btnMore.Text = resources.GetString("btnMore.Text");
			this.btnMore.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMore.TextAlign")));
			this.btnMore.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("btnMore.TextImageRelation")));
			this.btnMore.UseVisualStyleBackColor = false;
			this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
			// 
			// btnSend
			// 
			this.btnSend.AccessibleDescription = resources.GetString("btnSend.AccessibleDescription");
			this.btnSend.AccessibleName = resources.GetString("btnSend.AccessibleName");
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSend.Anchor")));
			this.btnSend.AutoSize = ((bool)(resources.GetObject("btnSend.AutoSize")));
			this.btnSend.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("btnSend.AutoSizeMode")));
			this.btnSend.BackColor = System.Drawing.Color.Transparent;
			this.btnSend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSend.BackgroundImage")));
			this.btnSend.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("btnSend.BackgroundImageLayout")));
			this.btnSend.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSend.Dock")));
			this.btnSend.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSend.FlatStyle")));
			this.btnSend.Font = ((System.Drawing.Font)(resources.GetObject("btnSend.Font")));
			this.btnSend.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSend.ImageAlign")));
			this.btnSend.ImageIndex = ((int)(resources.GetObject("btnSend.ImageIndex")));
			this.btnSend.ImageKey = resources.GetString("btnSend.ImageKey");
			this.btnSend.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSend.ImeMode")));
			this.btnSend.Location = ((System.Drawing.Point)(resources.GetObject("btnSend.Location")));
			this.btnSend.MaximumSize = ((System.Drawing.Size)(resources.GetObject("btnSend.MaximumSize")));
			this.btnSend.Name = "btnSend";
			this.btnSend.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSend.RightToLeft")));
			this.btnSend.Size = ((System.Drawing.Size)(resources.GetObject("btnSend.Size")));
			this.btnSend.TabIndex = ((int)(resources.GetObject("btnSend.TabIndex")));
			this.btnSend.Text = resources.GetString("btnSend.Text");
			this.btnSend.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSend.TextAlign")));
			this.btnSend.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("btnSend.TextImageRelation")));
			this.btnSend.UseVisualStyleBackColor = false;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// lblMessage
			// 
			this.lblMessage.AccessibleDescription = resources.GetString("lblMessage.AccessibleDescription");
			this.lblMessage.AccessibleName = resources.GetString("lblMessage.AccessibleName");
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMessage.Anchor")));
			this.lblMessage.AutoSize = ((bool)(resources.GetObject("lblMessage.AutoSize")));
			this.lblMessage.BackColor = System.Drawing.Color.Transparent;
			this.lblMessage.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("lblMessage.BackgroundImageLayout")));
			this.lblMessage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMessage.Dock")));
			this.lblMessage.Font = ((System.Drawing.Font)(resources.GetObject("lblMessage.Font")));
			this.lblMessage.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMessage.ImageAlign")));
			this.lblMessage.ImageIndex = ((int)(resources.GetObject("lblMessage.ImageIndex")));
			this.lblMessage.ImageKey = resources.GetString("lblMessage.ImageKey");
			this.lblMessage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMessage.ImeMode")));
			this.lblMessage.Location = ((System.Drawing.Point)(resources.GetObject("lblMessage.Location")));
			this.lblMessage.MaximumSize = ((System.Drawing.Size)(resources.GetObject("lblMessage.MaximumSize")));
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMessage.RightToLeft")));
			this.lblMessage.Size = ((System.Drawing.Size)(resources.GetObject("lblMessage.Size")));
			this.lblMessage.TabIndex = ((int)(resources.GetObject("lblMessage.TabIndex")));
			this.lblMessage.Text = resources.GetString("lblMessage.Text");
			this.lblMessage.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMessage.TextAlign")));
			// 
			// txtMessage
			// 
			this.txtMessage.AccessibleDescription = resources.GetString("txtMessage.AccessibleDescription");
			this.txtMessage.AccessibleName = resources.GetString("txtMessage.AccessibleName");
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtMessage.Anchor")));
			this.txtMessage.BackColor = System.Drawing.Color.Black;
			this.txtMessage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtMessage.BackgroundImage")));
			this.txtMessage.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("txtMessage.BackgroundImageLayout")));
			this.txtMessage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtMessage.Dock")));
			this.txtMessage.Font = ((System.Drawing.Font)(resources.GetObject("txtMessage.Font")));
			this.txtMessage.ForeColor = System.Drawing.Color.Lime;
			this.txtMessage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtMessage.ImeMode")));
			this.txtMessage.Location = ((System.Drawing.Point)(resources.GetObject("txtMessage.Location")));
			this.txtMessage.MaximumSize = ((System.Drawing.Size)(resources.GetObject("txtMessage.MaximumSize")));
			this.txtMessage.MaxLength = ((int)(resources.GetObject("txtMessage.MaxLength")));
			this.txtMessage.Multiline = ((bool)(resources.GetObject("txtMessage.Multiline")));
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.PasswordChar = ((char)(resources.GetObject("txtMessage.PasswordChar")));
			this.txtMessage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtMessage.RightToLeft")));
			this.txtMessage.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtMessage.ScrollBars")));
			this.txtMessage.Size = ((System.Drawing.Size)(resources.GetObject("txtMessage.Size")));
			this.txtMessage.TabIndex = ((int)(resources.GetObject("txtMessage.TabIndex")));
			this.txtMessage.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtMessage.TextAlign")));
			this.txtMessage.WordWrap = ((bool)(resources.GetObject("txtMessage.WordWrap")));
			this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
			// 
			// frmAbout
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoSize = ((bool)(resources.GetObject("$this.AutoSize")));
			this.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("$this.AutoSizeMode")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("$this.BackgroundImageLayout")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.picApplicationIcon);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.btnMore);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lnkLink);
			this.Controls.Add(this.btnSysteminfo);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblApplicationName);
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.Opacity = 0.99D;
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.RightToLeftLayout = ((bool)(resources.GetObject("$this.RightToLeftLayout")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closed += new System.EventHandler(this.frmAbout_Closed);
			this.Load += new System.EventHandler(this.frmAbout_Load);
			((System.ComponentModel.ISupportInitialize)(this.picApplicationIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
	}
}


