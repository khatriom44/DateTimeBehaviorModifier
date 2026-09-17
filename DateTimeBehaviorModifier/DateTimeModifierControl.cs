using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace DateTimeBehaviorModifier
{
    public partial class DateTimeModifierControl : PluginControlBase
    {
        private Settings mySettings;

        public DateTimeModifierControl()
        {
            InitializeComponent();

            // Populate Target Format column options (Index 2)
            if (dataGridView1.Columns[2] is DataGridViewComboBoxColumn formatCol)
            {
                formatCol.Items.Clear();
                formatCol.Items.AddRange("DateOnly", "DateAndTime");
            }

            // Populate Target Behavior column options (Index 3) with baseline set
            if (dataGridView1.Columns[3] is DataGridViewComboBoxColumn behaviorCol)
            {
                behaviorCol.Items.Clear();
                behaviorCol.Items.AddRange("DateOnly", "UserLocal", "TimeZoneIndependent");
            }

            // Hook up events programmatically to guarantee they fire
            dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.DataError += DataGridView1_DataError;
            btnUpdate.Click += btnUpdate_Click;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suppress default WinForms validation popup dialogs safely
            e.ThrowException = false;
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // If the user changes the Target Format (Column 2)
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                var formatCell = dataGridView1.Rows[e.RowIndex].Cells[2];
                var behaviorCell = dataGridView1.Rows[e.RowIndex].Cells[3] as DataGridViewComboBoxCell;

                if (behaviorCell != null && formatCell.Value != null)
                {
                    string selectedFormat = formatCell.Value.ToString();

                    behaviorCell.Items.Clear();

                    if (selectedFormat == "DateOnly")
                    {
                        // DateOnly format allows all three options
                        behaviorCell.Items.AddRange("DateOnly", "UserLocal", "TimeZoneIndependent");
                        behaviorCell.Value = "DateOnly";
                    }
                    else // DateAndTime
                    {
                        // DateAndTime format allows only UserLocal or TimeZoneIndependent
                        behaviorCell.Items.AddRange("UserLocal", "TimeZoneIndependent");
                        behaviorCell.Value = "UserLocal";
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbTables.SelectedItem == null)
            {
                MessageBox.Show("Please select a table first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedTable = cmbTables.SelectedItem.ToString();
            var selectedRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                {
                    selectedRows.Add(row);
                }
            }

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one field to update.", "No Rows Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            rtbLogs.AppendText($"> Starting update for {selectedRows.Count} field(s) on table '{selectedTable}'...\n");

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating field behaviors in Dataverse...",
                Work = (worker, args) =>
                {
                    int successCount = 0;
                    foreach (var row in selectedRows)
                    {
                        string attributeName = row.Cells[1].Value?.ToString();
                        string targetBehaviorStr = row.Cells[3].Value?.ToString() ?? "UserLocal";
                        string targetFormatStr = row.Cells[2].Value?.ToString() ?? "DateAndTime";

                        if (string.IsNullOrEmpty(attributeName)) continue;

                        var attributeMetadata = new Microsoft.Xrm.Sdk.Metadata.DateTimeAttributeMetadata
                        {
                            LogicalName = attributeName,
                            DateTimeBehavior = targetBehaviorStr,
                            Format = targetFormatStr == "DateOnly"
                                ? Microsoft.Xrm.Sdk.Metadata.DateTimeFormat.DateOnly
                                : Microsoft.Xrm.Sdk.Metadata.DateTimeFormat.DateAndTime
                        };

                        var updateRequest = new UpdateAttributeRequest
                        {
                            EntityName = selectedTable,
                            Attribute = attributeMetadata,
                            MergeLabels = false
                        };

                        Service.Execute(updateRequest);
                        successCount++;
                    }

                    args.Result = successCount;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show($"Error during update: {args.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rtbLogs.AppendText($"> Update failed: {args.Error.Message}\n");
                        return;
                    }

                    int count = (int)args.Result;
                    MessageBox.Show($"Successfully updated {count} field behavior(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rtbLogs.AppendText($"> Successfully updated {count} field behavior(s).\n");
                }
            });
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetAccounts);
        }

        private void GetAccounts()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting accounts",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("account")
                    {
                        TopCount = 50
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result != null)
                    {
                        MessageBox.Show($"Found {result.Entities.Count} accounts");
                    }
                }
            });
        }

        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void tsbLoadTables_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to a Dataverse environment first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving Dataverse tables...",
                Work = (worker, args) =>
                {
                    var request = new RetrieveAllEntitiesRequest
                    {
                        EntityFilters = Microsoft.Xrm.Sdk.Metadata.EntityFilters.Entity,
                        RetrieveAsIfPublished = true
                    };

                    var response = (RetrieveAllEntitiesResponse)Service.Execute(request);
                    args.Result = response.EntityMetadata;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (args.Result is Microsoft.Xrm.Sdk.Metadata.EntityMetadata[] metadata)
                    {
                        cmbTables.Items.Clear();

                        var tables = metadata
                            .Where(x => x.IsCustomizable.Value == true)
                            .OrderBy(x => x.LogicalName)
                            .ToList();

                        foreach (var table in tables)
                        {
                            cmbTables.Items.Add(table.LogicalName);
                        }

                        rtbLogs.AppendText($"> Successfully loaded {tables.Count} tables.\n");
                    }
                }
            });
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTables.SelectedItem == null) return;

            string selectedTable = cmbTables.SelectedItem.ToString();
            rtbLogs.AppendText($"> Fetching Date/Time fields for {selectedTable}...\n");

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Retrieving attributes for {selectedTable}...",
                Work = (worker, args) =>
                {
                    var request = new RetrieveEntityRequest
                    {
                        LogicalName = selectedTable,
                        EntityFilters = Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes,
                        RetrieveAsIfPublished = true
                    };

                    var response = (RetrieveEntityResponse)Service.Execute(request);
                    args.Result = response.EntityMetadata;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (args.Result is Microsoft.Xrm.Sdk.Metadata.EntityMetadata meta)
                    {
                        dataGridView1.Rows.Clear();

                        // Filter to include ONLY custom DateTime attributes
                        var dateTimeAttributes = meta.Attributes
                            .OfType<Microsoft.Xrm.Sdk.Metadata.DateTimeAttributeMetadata>()
                            .Where(x => x.IsCustomAttribute.HasValue && x.IsCustomAttribute.Value == true)
                            .OrderBy(x => x.LogicalName)
                            .ToList();

                        foreach (var attr in dateTimeAttributes)
                        {
                            string formatVal = attr.Format?.ToString() ?? "DateAndTime";
                            string behaviorVal = attr.DateTimeBehavior?.Value.ToString() ?? "UserLocal";

                            int rowIndex = dataGridView1.Rows.Add(
                                false,
                                attr.LogicalName,
                                formatVal,
                                behaviorVal
                            );

                            if (dataGridView1.Rows[rowIndex].Cells[3] is DataGridViewComboBoxCell behaviorCell)
                            {
                                behaviorCell.Items.Clear();
                                if (formatVal == "DateOnly")
                                {
                                    behaviorCell.Items.AddRange("DateOnly", "UserLocal", "TimeZoneIndependent");
                                }
                                else
                                {
                                    behaviorCell.Items.AddRange("UserLocal", "TimeZoneIndependent");
                                }

                                behaviorCell.Value = behaviorCell.Items.Contains(behaviorVal) ? behaviorVal : behaviorCell.Items[0];
                            }
                        }

                        rtbLogs.AppendText($"> Loaded {dateTimeAttributes.Count} custom Date/Time fields.\n");
                    }
                }
            });
        }
    }
}