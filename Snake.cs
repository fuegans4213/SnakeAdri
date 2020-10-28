using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeAdri
{
    public class Snake
    {
        #region Attributs privés
        private Brush _couleursnake = Brushes.Green;
        private Brush _couleurtetesnake = Brushes.Red;

        private Random _rnd = new Random();

        private Point _pointdedepart;

        private int _tailletete = 8; //(int)SIZE.THICK;

        private Canvas _snakecanvas = new Canvas();

        private Point _positioncourante = new Point();
        private enum MOVINGDIRECTION
        {
            UPWARDS = 8,
            DOWNWARDS = 2,
            TOLEFT = 4,
            TORIGHT = 6
        };

        #endregion

        #region Attributs public
        public Point PositionCourante
        {
            get { return _positioncourante; }
            set { _positioncourante = value; }
        }

        #endregion
        // J'ai mis les autres vitesse en commentaire. Celle-ci sont simplement impossible avec un movement de += 10.

        // private TimeSpan FAST = new TimeSpan(1);
        // private TimeSpan MODERATE = new TimeSpan(10000);
        // private TimeSpan SLOW = new TimeSpan(50000);
        //private TimeSpan DAMNSLOW = new TimeSpan(500000);

        public Snake()
        {
            //Génére le point de départ du serpent
            _pointdedepart = new Point(_rnd.Next(10, 640), _rnd.Next(10, 440));

            //On défini la position courante à la position de départ lors de la construction du snake
            _positioncourante = _pointdedepart;
        }
        /// <summary>
        /// Permet d'afficher le serpent dans le canvas
        /// </summary>
        /// <param name="levelcanvas">Canvas de la fenêtre</param>
        public void AfficherSnake(Canvas levelcanvas)
        {

            //Paramètrage de la tête du serpent
            Rectangle tete = new Rectangle();
            tete.Fill = _couleurtetesnake;
            tete.Width = _tailletete;
            tete.Height = _tailletete;

            Canvas.SetTop(tete, _positioncourante.Y);
            Canvas.SetLeft(tete, _positioncourante.X);

            //Paramètrage du du serpent
            Ellipse corp = new Ellipse();
            corp.Fill = _couleursnake;
            corp.Width = _tailletete;
            corp.Height = _tailletete;

            Canvas.SetTop(corp, _positioncourante.Y);
            Canvas.SetLeft(corp, _positioncourante.X-8);

            //Intégration de la tête et du courp du serpent 
            levelcanvas.Children.Add(tete);
            levelcanvas.Children.Add(corp);

            int count = levelcanvas.Children.Count;

            // snakePoints.Add(currentposition);

            //if (count > length)
            //{
            //    paintCanvas.Children.RemoveAt(count - length);
            //    snakePoints.RemoveAt(count - length);
            //}

        }


        //private int direction = 0;

        //private int previousDirection = 0;

        //private int headSize = (int)SIZE.THICK;



        //private int length = 10;
        //private int score = 0;


    }
}
