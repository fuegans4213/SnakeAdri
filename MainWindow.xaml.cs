using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeAdri
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //Création du serpent
            Snake snakegame = new Snake();

            DispatcherTimer GameTimer = new DispatcherTimer();

            GameTimer.Tick += new EventHandler((sender, e) => snakegame.Snaketimer(sender, e, levelcanvas));

            GameTimer.Interval = new TimeSpan(900000); ;
            GameTimer.Start();

            this.KeyDown += new KeyEventHandler((sender, e) => snakegame.OnAppuieSurTouche(sender, e));

            //Affichage du Snake initialement
            snakegame.AfficherSnake(levelcanvas);
            //On insert le premier Fruit 
            snakegame.AfficherFruit(0, levelcanvas);


        }
    }
}
