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
using WpfApp2.Сlasses;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Сlasses.PersonInfo Player = new Сlasses.PersonInfo("Student", 100, 10, 1, 0, 0, 5);
        public List<PersonInfo> Enemys = new List<PersonInfo>();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Сlasses.PersonInfo Enemy;
        public MainWindow()
        {
            InitializeComponent();
            UserInfoPlayer();

            Enemys.Add(new PersonInfo("Маленький монстр", 100, 10, 1, 10, 15, 10));
            Enemys.Add(new PersonInfo("Средний монстр", 120, 15, 1, 15, 30, 15));
            Enemys.Add(new PersonInfo("Большой монстр", 150, 20, 1, 20, 40, 20));

            dispatcherTimer.Tick += AttackPlayer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            SelectEnemy();

        }
        private void AttackPlayer(object sender, EventArgs e)
        {
            Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
            UserInfoPlayer();
        }
        
        public void SelectEnemy()
        {
            int Id = new Random().Next(0, Enemys.Count);
            Enemy = new PersonInfo(
                Enemys[Id].Name,
                Enemys[Id].Health,
                Enemys[Id].Armor,
                Enemys[Id].Level,
                Enemys[Id].Glasses,
                Enemys[Id].Money,
                Enemys[Id].Damage
                );
        }
        public void UserInfoPlayer()
        {
            if (Player.Glasses > 100 * Player.Level)
            {
                Player.Level++;
                Player.Glasses = 0;
                Player.Health += 100;
                Player.Damage++;
                Player.Armor++;
            }
            playerHealth.Content = "Жизненные показатели: " + Player.Health;
            playerArmor.Content = "Броня: " + Player.Armor;
            playerLevel.Content = "Уровень: " + Player.Level;
            playerGlasses.Content = "Опыт: " + Player.Glasses;
            playerMoney.Content = "Монеты: " + Player.Money;
        }

        private void AttackEnemy(object sender, MouseButtonEventArgs e)
        {
            Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));
            if(Enemy.Health <= 0)
            {
                Player.Glasses += Enemy.Glasses;

                int rndm = new Random().Next(0, 20);
                Player.Money += Enemy.Money + (Enemy.Money * rndm/100);
                UserInfoPlayer();
                SelectEnemy();
            }
            else
            {
                enemyHealth.Content = "Жизненые показатели: " + Enemy.Health;
                enemyArmor.Content = "Броня: " + Enemy.Armor;
            }
            //2 вариант
            // 5% шансом игрок будет похищать здоровье врага, которое снес ему при ударе
            int probability = new Random().Next(0, 100);
            if (probability > 5) Player.Health += Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));
            

        }
    }
}
