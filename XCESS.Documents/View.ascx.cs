using System;
using System.Collections.Generic;
using DNNtc;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Security.Permissions;

namespace XCESS.Documents
{
    /// <summary>
    ///     The View class displays the content
    /// </summary>
    [DNNtc.ModuleDependencies(DNNtc.ModuleDependency.CoreVersion,"08.00.04")]
    [DNNtc.ModuleControlProperties("", "Module title", DNNtc.ControlType.View, "", true, false)]
    public partial class View : PortalModuleBase
    {
        private IFolderInfo folder;

        private void ShowLoggedOnUserOrWarningIfNotLoggedIn()
        {

            // Get logged on user
            var user = UserController.Instance.GetCurrentUserInfo();
            var currentUserName = string.Empty;
            var cssClass = "xecMyModuleTitle";
     
            // Check if a valid user is logged on
            if (user.UserID == -1)
            {
                // No - Not logged on
                cssClass += " xecNotLoggedIn";

                // The user name is replaced by a warning message
                currentUserName = LocalizeString("NotLoggedInWarning");

                lblMyModuleTitle.Visible = false;
                //this.TreeView1.Visible = false;
               
            }
            else
            {
                // Yes - show the users display name
                currentUserName = user.DisplayName;
                //TreeView1.Visible = true;
            }

            // Show a localized module title
            lblMyModuleTitle.Text = LocalizeString("MyModuleTitle");

            // Show the user name
            lblCurrentUser.CssClass = cssClass;
            lblCurrentUser.Text = currentUserName;
        }


        private IEnumerable<IFolderInfo> GetChildFolders(int folderID)
        {
            var parentFolder = FolderManager.Instance.GetFolder(folderID);
            var folders = FolderManager.Instance.GetFolders(parentFolder); 

            var viewableFolders = new List<FolderInfo>();
            foreach (FolderInfo folder in folders)
            {
                if (FolderPermissionController.CanViewFolder (folder)==true)
                    {
                    viewableFolders.Add(folder);
                }
            }
                return viewableFolders;
        }

        private void ConvertFilesToNodes(TreeNode node, IEnumerable<IFileInfo> files)
        {
            var nodes = node.ChildNodes;
            foreach (var file in files)
            {
                var value = $"FileId={file.FileId}";
                var text = $"<span class='file'>{file.FileName}</span>";
                var treeNode = new TreeNode(text, value);
                var fileUrl = FileManager.Instance.GetUrl(file);
                treeNode.NavigateUrl = fileUrl;
                treeNode.Target = "_blank";
                nodes.Add(treeNode);
            }
        }

        private void ConvertFoldersToNodes(IEnumerable<IFolderInfo> folders)
        {
            var nodes = this.TreeView1.Nodes;
            foreach (var folder in folders)
            {
                var text = $"<span class='folder'>{folder.DisplayName}</span>";
                var treeNode = new TreeNode(text, folder.FolderID.ToString());
                nodes.Add(treeNode);
            }
        }

        private void DownloadFile(TreeNode selectedNode)
        {
            var fileIdString = selectedNode.Value.Substring(7);
            var fileId = Convert.ToInt32(fileIdString);
            var file = FileManager.Instance.GetFile(fileId);
            var fileUrl = FileManager.Instance.GetUrl(file);
            this.Response.Redirect(fileUrl); 
        }

        private void InitializeTreeView()
        {
            if (this.Settings["xec_FolderID"]!=null && !String.IsNullOrEmpty(this.Settings["xec_FolderID"].ToString()))
            {
                var viewableFolders = this.GetChildFolders(Convert.ToInt32(this.Settings["xec_FolderID"]));
                this.TreeView1.Nodes.Clear();
                this.ConvertFoldersToNodes(viewableFolders);
                this.TreeView1.Visible = true;
            }
        }

        private void IfFirstTimeThenAddTreeViewNodesForSelectedNode(TreeNode selectedNode)
        {
            if (selectedNode!=null && selectedNode.ChildNodes.Count == 0)
            {
                var folderId = int.Parse(selectedNode.Value);
                var folder = FolderManager.Instance.GetFolder(folderId);
                var files = FolderManager.Instance.GetFiles(folder);
                this.ConvertFilesToNodes(selectedNode, files);
                selectedNode.Expand();             
            }
           else if (selectedNode != null)
            {
                selectedNode.ChildNodes.Clear();
                selectedNode.Collapse();
            }
        }


        #region Event Handlers

        /// <summary>
        ///     Runs when the control is loaded
        /// </summary>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowLoggedOnUserOrWarningIfNotLoggedIn();
                if (!this.IsPostBack)
                {
                    this.InitializeTreeView();
                }
                else
                {
                    this.ClearParentNodes();

                    this.IfFirstTimeThenAddTreeViewNodesForSelectedNode(this.TreeView1.SelectedNode);
                }
            }
        
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

      private void ClearParentNodes ()
        {
            foreach (TreeNode treeNode in this.TreeView1.Nodes)
            {
                if(treeNode.Selected == false)
                {
                    treeNode.ChildNodes.Clear();
                    treeNode.Collapse();
                } 
            }
        }
        #endregion

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            var selectedNode = this.TreeView1.SelectedNode;

            //var value = selectedNode.Value;

            // Add files for selected folder (node) if not yet added
            this.IfFirstTimeThenAddTreeViewNodesForSelectedNode(selectedNode);
            // Collapse all nodes
            this.TreeView1.CollapseAll();
            // Ensure that the selected node is expanded.
            selectedNode.Expand();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TreeView1.CollapseAll();
        }

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            this.IfFirstTimeThenAddTreeViewNodesForSelectedNode(e.Node);
        }
    }
}