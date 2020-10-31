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
        private List<Point> _fruitPoints = new List<Point>();
        private List<Point> _serpentpoints = new List<Point>();

        private Brush _couleurserpent = Brushes.Green;

        private Random _rnd = new Random();

        private Point _pointdedepart;
        private Point _positioncourante;

        private int _tailleelements = 8;

        private int _longueurserpent = 10;

        private int _direction = 0;

        private int _directionPrecedente = 0;

        private int _score = 0;

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

        public Snake()
        {
            //Génére le point de départ du serpent
            _pointdedepart = new Point(_rnd.Next(20, 630), _rnd.Next(20, 400));

            //On défini la position courante à la position de départ lors de la construction du snake
            _positioncourante = _pointdedepart;
        }

        /// <summary>
        /// Permet d'afficher le serpent dans le canvas à une position donnée
        /// </summary>
        /// <param name="levelcanvas">Canvas de la fenêtre</param>
        public void AfficherSnake(Canvas levelcanvas)
        {
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

        public void AfficherFruit(int index, Canvas levelcanvas)
        {
             Point fruitPoint = new Point(_rnd.Next(20, 630), _rnd.Next(20, 400));

            //Création graphique du fruit
            Ellipse fruit = new Ellipse();

            fruit.Fill = Brushes.DarkRed;
            fruit.Width = _tailleelements;
            fruit.Height = _tailleelements;

            Canvas.SetTop(fruit, fruitPoint.Y);
            Canvas.SetLeft(fruit, fruitPoint.X);

            //levelcanvas.Children.Add(newEllipse);
            levelcanvas.Children.Insert(index, fruit);
            _fruitPoints.Insert(index, fruitPoint);

        }

        public void Snaketimer(object sender, EventArgs e, Canvas levelcanvas)
        {
            switch (_direction)
            {
                case (int)_deplacement.BAS:
                    _positioncourante.Y += 8;
                    AfficherSnake(levelcanvas);
                    break;
                case (int)_deplacement.HAUT:
                    _positioncourante.Y -= 8;
                    AfficherSnake(levelcanvas);
                    break;
                case (int)_deplacement.GAUCHE:
                    _positioncourante.X -= 8;
                    AfficherSnake(levelcanvas);
                    break;
                case (int)_deplacement.DROITE:
                    _positioncourante.X += 8;
                    AfficherSnake(levelcanvas);
                    break;
            }

            // si le serpent va en dehors des limites du tableau perdu. 
            if ((_positioncourante.X < 5) || (_positioncourante.X > 620) ||
                (_positioncourante.Y < 5) || (_positioncourante.Y > 395))
                Perdu();

            int i = 0;
            foreach (Point point in _fruitPoints)
            {

                if ((Math.Abs(point.X - _positioncourante.X) < _tailleelements) &&
                    (Math.Abs(point.Y - _positioncourante.Y) < _tailleelements))
                {
                    //ajoute des eélement au corpsdu serpent
                    _longueurserpent += 5;
                    _score += 10;
                    _fruitPoints.RemoveAt(i);
                    levelcanvas.Children.RemoveAt(i);
                    AfficherFruit(i,levelcanvas);
                    break;
                }
                i++;
            }

            //permet de savoir si on ne se mange pas
            for (int q = 0; q < (_serpentpoints.Count - 1); q++)
            {
                Point point = new Point(_serpentpoints[q].X, _serpentpoints[q].Y);
                if ((Math.Abs(point.X - _positioncourante.X) < (_tailleelements)) &&
                     (Math.Abs(point.Y - _positioncourante.Y) < (_tailleelements)))
                {
                    Perdu();
                    break;
                }
            }
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
        
        
        private void Perdu()
        {
            MessageBox.Show("Perdu! ton score : " + _score.ToString() + " \nAppui sur entrer pour recomencer", "Perdu", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

    }
}
