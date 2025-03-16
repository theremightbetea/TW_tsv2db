using System.DirectoryServices;
using System.IO.Packaging;
using EtruscanUnitDb;

namespace EtruscanUnitDbGUI;

public partial class EtruscanUnitDbGUIwindow : Form
{
public string serverName;
public string dbName;
public string dataSourceName;
public string tableName;
public string filePath;
public EtruscanUnitDbGUIwindow()
    {
        InitializeComponent();
        this.Shown += BrowseButton_Create;
    }

    private void BrowseButton_Create(object sender, EventArgs e)
    {
        Button browseButton = new Button();
        browseButton.Location = new Point(10, 10);
		browseButton.AutoSize = true;
        browseButton.Text = "Select your tsv-file";
        this.Controls.Add(browseButton);
	//browseButton.Size = ;

        Label selectedFileLable = new Label();
        selectedFileLable.AutoSize = true;
        selectedFileLable.Text = "Click to select your tsv-file";
        selectedFileLable.Location = new Point(10, 40);
        this.Controls.Add(selectedFileLable);

	Label ErrorLabel = new Label();
	ErrorLabel.AutoSize = true;
	ErrorLabel.Location = new Point(10, 210);
	this.Controls.Add(ErrorLabel);

	List<TextBox> textBoxes = new List<TextBox>();

	/*
	TextBox enterServerName = new TextBox();
	enterServerName.Location = new Point(10,80);
	enterServerName.Size = new Size(200,10);
	string enterServerNameText = "Enter the name of your server";
	enterServerName.Text = enterServerNameText;
	enterServerName.ForeColor = Color.LightGray;
	textBoxes.Add(enterServerName);

	enterServerName.Enter += (sender, args) => 
	{
		if(enterServerName.Text == enterServerNameText)
		{
			enterServerName.Text = "";
			enterServerName.ForeColor = Color.Black;
		}
	};

	enterServerName.Leave += (sender, args) =>
	{
		if(enterServerName.Text == enterServerNameText || String.IsNullOrEmpty(enterServerName.Text))
		{
			enterServerName.Text = enterServerNameText;
			enterServerName.ForeColor = Color.LightGray;
		}
	};
	*/

    TextBox enterDbName = new TextBox();
	enterDbName.Location = new Point(10, 120);
	enterDbName.Size = new Size(200,10);
	string enterDbNameText = "Enter the name of your database";
	enterDbName.Text = enterDbNameText;
	enterDbName.ForeColor = Color.LightGray;
	textBoxes.Add(enterDbName);

	enterDbName.Enter += (sender, args) => 
	{
		if(enterDbName.Text == enterDbNameText)
		{
			enterDbName.Text = "";
			enterDbName.ForeColor = Color.Black;
		}
	};

	enterDbName.Leave += (sender, args) =>
	{
		if(enterDbName.Text == enterDbNameText || String.IsNullOrEmpty(enterDbName.Text))
		{
			enterDbName.Text = enterDbNameText;
			enterDbName.ForeColor = Color.LightGray;
		}
	};

	TextBox enterDataSourceName = new TextBox();
	enterDataSourceName.Location = new Point(10, 80);
	enterDataSourceName.Size = new Size(200,10);
	string enterDataSourceNameText = "Enter the name of your datasource";
	enterDataSourceName.Text = enterDataSourceNameText;
	enterDataSourceName.ForeColor = Color.LightGray;
	textBoxes.Add(enterDataSourceName);

	enterDataSourceName.Enter += (sender, args) => 
	{
		if(enterDataSourceName.Text == enterDataSourceNameText)
		{
			enterDataSourceName.Text = "";
			enterDataSourceName.ForeColor = Color.Black;
		}
	};

	enterDataSourceName.Leave += (sender, args) =>
	{
		if(enterDataSourceName.Text == enterDataSourceNameText || String.IsNullOrEmpty(enterDataSourceName.Text))
		{
			enterDataSourceName.Text = enterDataSourceNameText;
			enterDataSourceName.ForeColor = Color.LightGray;
		}
	};

	TextBox enterTableName = new TextBox();
	enterTableName.Location = new Point(10, 160);
	enterTableName.Size = new Size(200, 10);
	string enterTableNameText = "Enter the name of your table";
	enterTableName.Text = enterTableNameText;
	enterTableName.ForeColor = Color.LightGray;
	textBoxes.Add(enterTableName);

		enterTableName.Enter += (sender, args) =>
	{
		if (enterTableName.Text == enterTableNameText)
		{
			enterTableName.Text = "";
			enterTableName.ForeColor = Color.Black;
		}
	};

		enterTableName.Leave += (sender, args) =>
	{
		if (enterTableName.Text == enterTableNameText || String.IsNullOrEmpty(enterTableName.Text))
		{
			enterTableName.Text = enterTableNameText;
			enterTableName.ForeColor = Color.LightGray;
		}
	};

	Button etrDbButton = new Button();
	etrDbButton.Location = new Point(10, 200);
	etrDbButton.AutoSize = true;
	etrDbButton.Text = "Insert the contents of your tsv-file into the database";

    browseButton.Click += (sender, args) => 
    {
        var FileSelection = new OpenFileDialog();
        FileSelection.Title = "Select your tsv";
        FileSelection.DefaultExt = "tsv";
        FileSelection.Filter = "*.tsv |*.tsv";
        //DialogResult dR = FileSelection.ShowDialog();
        if(FileSelection.ShowDialog() == DialogResult.OK)
        {
            filePath = FileSelection.FileName;
            //Console.WriteLine(filePath);
            selectedFileLable.Text = "Selected file:";
            Label pathLabel = new Label();
            pathLabel.AutoSize = true;
            pathLabel.Text = filePath;
            pathLabel.Location = new Point(10, 60);
            this.Controls.Add(pathLabel);
            this.Controls.Add(etrDbButton);
	//this.Controls.Add(enterServerName);
	this.Controls.Add(enterDbName);
	this.Controls.Add(enterDataSourceName);
	this.Controls.Add(enterTableName);
        }
    };

	etrDbButton.Click += (sender, args) =>
	{
		//serverName = enterServerName.Text;
		dbName = enterDbName.Text;
		dataSourceName = enterDataSourceName.Text;
		tableName = enterTableName.Text;
		/*
		Console.WriteLine(serverName);
		Console.WriteLine(dbName);
		Console.WriteLine(dataSourceName);
		*/
		string connectionString = @"data source=" + dataSourceName + ";initial catalog=" + dbName + ";trusted_connection=true;MultipleActiveResultSets=True";
		bool incInput = false;
		if(dbName != enterDbNameText && dataSourceName != enterDataSourceNameText && tableName != enterTableNameText)
		{
			try
			{
				var run = new UnitTablesToDb(filePath, connectionString, tableName);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				ErrorLabel.Text = "Incorrect input!";
			}
		}
		else if(String.IsNullOrEmpty(dbName) || String.IsNullOrEmpty(dataSourceName) || dbName == enterDbNameText || dataSourceName == enterDataSourceNameText || tableName == enterTableNameText)
		{
			ErrorLabel.Text = "Insufficient input!";
		}
		else
		{
			ErrorLabel.Text = "Unknown Error!";
		}
	};
		
    }
}
    
