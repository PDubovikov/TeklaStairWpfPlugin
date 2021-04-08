using System;
using System.Windows;
using System.ComponentModel;
using Tekla.Structures.Dialog;

namespace TeklaStairWpfPlugin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PluginWindowBase
    {
        public MainWindowViewModel dataModel;

        public MainWindow(MainWindowViewModel DataModel)
        {
            InitializeComponent();
            dataModel = DataModel;
        }

        private void WPFOkApplyModifyGetOnOffCancel_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void WPFOkApplyModifyGetOnOffCancel_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WPFOkApplyModifyGetOnOffCancel_GetClicked(object sender, EventArgs e)
        {
            this.Get();
        }

        private void WPFOkApplyModifyGetOnOffCancel_ModifyClicked(object sender, EventArgs e)
        {
            this.Modify();
        }

        private void WPFOkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }

        private void WPFOkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }


        private void profileStringerCatalog_SelectClicked(object sender, EventArgs e)
        {
            
            this.profileStringerCatalog.SelectedProfile = this.dataModel.StringerProfile;
        }

        private void profileStringerCatalog_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.StringerProfile = this.profileStringerCatalog.SelectedProfile;
        }

        private void materialStringersCatalog_SelectClicked(object sender, EventArgs e)
        {
            this.materialStringersCatalog.SelectedMaterial = this.dataModel.StringerMaterial;
        }

        private void materialStringersCatalog_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.StringerMaterial = this.materialStringersCatalog.SelectedMaterial;
        }

        private void prflStringerCatalog_SelectClicked(object sender, EventArgs e)
        {
            this.prflStringerCatalog.SelectedProfile = this.dataModel.StringerProfile;
        }

        private void prflStringerCatalog_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.StringerProfile = this.prflStringerCatalog.SelectedProfile;
        }

        private void mtrlStringersCatalog_SelectClicked(object sender, EventArgs e)
        {
            this.mtrlStringersCatalog.SelectedMaterial = this.dataModel.StringerMaterial;
        }

        private void mtrlStringersCatalog_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.StringerMaterial = this.mtrlStringersCatalog.SelectedMaterial;
        }

        private void profileStringerCatalogExt_SelectClicked(object sender, EventArgs e)
        {
            this.profileStringerCatalogExt.SelectedProfile = this.dataModel.StringerProfile;
        }

        private void profileStringerCatalogExt_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.StringerProfile = this.profileStringerCatalogExt.SelectedProfile;
        }

        private void materialStringersCatalogExt_SelectClicked(object sender, EventArgs e)
        {
            this.materialStringersCatalogExt.SelectedMaterial = this.dataModel.StringerMaterial;
        }

        private void materialStringersCatalogExt_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.StringerMaterial = this.materialStringersCatalogExt.SelectedMaterial;
        }
    }
}
