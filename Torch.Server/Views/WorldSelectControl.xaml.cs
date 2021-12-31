using System.Windows.Controls;

namespace Torch.Server.Views
{
    /// <summary>
    /// Interaction logic for WorldSelectControl.xaml
    /// </summary>
    public partial class WorldSelectControl : UserControl
    {
        public WorldSelectControl()
        {
            InitializeComponent();
            //LoadWorlds();
        }

        public void LoadWorlds(string path = null)
        {
            WorldList.Items.Clear();
            var worlds = new MyLoadWorldInfoListResult(path);
            worlds.Task.Wait();

            foreach (var world in worlds.AvailableSaves)
            {
                WorldList.Items.Add(world.Item1);
            }
        }
    }
}
