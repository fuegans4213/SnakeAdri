using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeAdri
{
    public class Snake
    {
        #region Attributs privés
        //Liste de poitn afin de gérer la logique du jeux
        private List<Point> bonusPoints = new List<Point>();
        private List<Point> _serpentpoints = new List<Point>();

        private Brush _couleurserpent = Brushes.Green;
        private Brush _couleurteteserpent = Brushes.Red;

        private Random _rnd = new Random();

        private Point _pointdedepart;
        private Point _positioncourante = new Point();
        private Point _positiontete;

        private int _tailleelements = 8; //(int)SIZE.THICK;

        private int _longueurserpent = 5;

        private int _direction = 0;

        private int _directionPrecedente = 0;

        private Canvas _serpentcanvas = new Canvas();


        private enum _deplacement
        {
            HAUT = 8,
            BAS = 2,
            GAUCHE = 4,
            DROITE = 6
        };

        #endregion

        #region Attributs public

        #endregion
        // J'ai mis les autres vitesse en commentaire. Celle-ci sont simplement impossible avec un movement de += 10.

        // private TimeSpan FAST = new TimeSpan(1);
        // private TimeSpan MODERATE = new TimeSpan(10000);
        // private TimeSpan SLOW = new TimeSpan(50000);
        //private TimeSpan DAMNSLOW = new TimeSpan(500000);

        public Snake()
        {
            //Génére le point de départ du serpent
            _pointdedepart = new Point(_rnd.Next(20, 630), _rnd.Next(20, 400));

            //On défini la position courante à la position de départ lors de la construction du snake
            _positiontete = _pointdedepart;
            _positioncourante = new Point(_pointdedepart.X-8, _pointdedepart.Y);
        }

        /// <summary>
        /// Permet d'afficher le serpent dans le canvas à une position donnée
        /// </summary>
        /// <param name="levelcanvas">Canvas de la fenêtre</param>
        public void AfficherSnake(Canvas levelcanvas)
        {

            //Paramètrage de la tête du serpent
            Rectangle tete = new Rectangle
            {
                Fill = _couleurteteserpent,
                Width = _tailleelements,
                Height = _tailleelements
            };

            Canvas.SetTop(tete, _positiontete.Y);
            Canvas.SetLeft(tete, _positiontete.X);

            //Paramètrage du du serpent
            Ellipse corp = new Ellipse
            {
                Fill = _couleurserpent,
                Width = _tailleelements,
                Height = _tailleelements
            };

            Canvas.SetTop(corp, _positioncourante.Y);
            Canvas.SetLeft(corp, _positioncourante.X);


            int nbrelementserpent = levelcanvas.Children.Count;

            //Je supprime la tête du serpent si elle existe ,pour l'afficher à la position suivante 
            if ((nbrelementserpent > 0) && (levelcanvas.Children[0] is System.Windows.Shapes.Rectangle))
            {
               levelcanvas.Children.RemoveAt(0);
            }

            levelcanvas.Children.Insert(0,tete);
            levelcanvas.Children.Add(corp);



            //ajout du serpent dans la liste de points (logique du jeux)
            _serpentpoints.Add(_positioncourante);

            //On supprime un élément du serpent si on dépasse la taille du serpent
            if (nbrelementserpent > _longueurserpent)
            {
                levelcanvas.Children.RemoveAt(nbrelementserpent - _longueurserpent);
                _serpentpoints.RemoveAt(nbrelementserpent - _longueurserpent);
            }

        }

        public void timer_Tick(object sender, EventArgs e, Canvas levelcanvas)
        {
            //Console.WriteLine("tick tack !" + _longueurserpent++);
            /* Changer la valeur de base 1 vers 10. Ceci nous permet de bouger le snake sans pouvoir nous manger la tête comme dans la version de base. On bouge
             * donc comme si le tableau était fait de carré. Comme dans un vrai jeu de Snake. */
            switch (_direction)
            {
                case (int)_deplacement.BAS:
                    _positioncourante = _positiontete;
                    _positiontete.Y += 8;
                    AfficherSnake(levelcanvas);
                    break;
                case (int)_deplacement.HAUT:
                    _positioncourante = _positiontete;
                    _positiontete.Y -= 8;
                    AfficherSnake(levelcanvas);
                    break;
                case (int)_deplacement.GAUCHE:
                    _positioncourante = _positiontete;
                    _positiontete.X -= 8;
                    AfficherSnake(levelcanvas);
                    break;
                case (int)_deplacement.DROITE:
                    _positioncourante = _positiontete;
                    _positiontete.X += 8;
                    AfficherSnake(levelcanvas);
                    break;
            }

            //// changement de (currentPosition.Y > 380) à (currentPosition.Y > 375) car le snake pouvait sortir son corp de la moitié en dehors des limites du tableau lorsqu'il allait en bas. 
            //if ((currentPosition.X < 5) || (currentPosition.X > 620) ||
            //    (currentPosition.Y < 5) || (currentPosition.Y > 375))
            //    GameOver();

            //int n = 0;
            //foreach (Point point in bonusPoints)
            //{

            //    if ((Math.Abs(point.X - currentPosition.X) < headSize) &&
            //        (Math.Abs(point.Y - currentPosition.Y) < headSize))
            //    {
            //        // Modifier la "Length" du snake à 10. Cela permet de commencer avec une longueur de base acceptable.
            //        length += 10;
            //        score += 10;

            //        bonusPoints.RemoveAt(n);
            //        paintCanvas.Children.RemoveAt(n);
            //        paintBonus(n);
            //        break;
            //    }
            //    n++;
            //}

            ///* Lorsque je changeais les mouvement vers 10 (voir plus haut), l'ancien calcule permettais de passé au travers de notre corp et si notre excécution était très rapide,
            //nous pouvions simplement passé au travers de notre corp tout entier en ligne bien droite. Maintenant, un input bien placé comme celle-ci nous fera perdre, ce qui est logique. 

            // Bref, headSize*2 remplacé par -1 pour une mort instantanné sur n'importe quelle partie du snake.*/
            //for (int q = 0; q < (snakePoints.Count - 1); q++)
            //{
            //    Point point = new Point(snakePoints[q].X, snakePoints[q].Y);
            //    if ((Math.Abs(point.X - currentPosition.X) < (headSize)) &&
            //         (Math.Abs(point.Y - currentPosition.Y) < (headSize)))
            //    {
            //        GameOver();
            //        break;
            //    }
            //}
        }

        public void OnAppuieSurTouche(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Down:
                    if (_directionPrecedente != (int)_deplacement.HAUT)
                        _direction = (int)_deplacement.BAS;
                    break;
                case Key.Up:
                    if (_directionPrecedente != (int)_deplacement.BAS)
                        _direction = (int)_deplacement.HAUT;
                    break;
                case Key.Left:
                    if (_directionPrecedente != (int)_deplacement.DROITE)
                        _direction = (int)_deplacement.GAUCHE;
                    break;
                case Key.Right:
                    if (_directionPrecedente != (int)_deplacement.GAUCHE)
                        _direction = (int)_deplacement.DROITE;
                    break;
            }
            _directionPrecedente = _direction;

        }

    }
}
