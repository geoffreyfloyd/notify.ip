namespace IpChangeNotifyService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ipChangeNotifyServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ipChangeNotifyServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ipChangeNotifyServiceProcessInstaller
            // 
            this.ipChangeNotifyServiceProcessInstaller.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ipChangeNotifyServiceInstaller});
            this.ipChangeNotifyServiceProcessInstaller.Password = null;
            this.ipChangeNotifyServiceProcessInstaller.Username = null;
            // 
            // ipChangeNotifyServiceInstaller
            // 
            this.ipChangeNotifyServiceInstaller.Description = "IP Change Notify Service";
            this.ipChangeNotifyServiceInstaller.DisplayName = "IP Change Notify Service";
            this.ipChangeNotifyServiceInstaller.ServiceName = "IpChangeNotifyService";
            this.ipChangeNotifyServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ipChangeNotifyServiceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ipChangeNotifyServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ipChangeNotifyServiceInstaller;
    }
}