using System;
using System.IO;

namespace GeneratoreIATrainingDataV2
{
    class Program
    {
        static int PlayerHp;
        static int PlayerMana;
        static int PlayerAttack;
        static int PlayerIntelligence;
        static int PlayerMagicCost;
        static int EnemyHp;
        static int EnemyMana;
        static int EnemyAttack;
        static int EnemyIntelligence;
        static int HasManaPotion; //1=true 0=false
        static int HasHpPotion; //1=true 0=false
        static int EnemyMagicHealCost;
        static int EnemyMagicDamageCost;
        static void Main(string[] args)
        {
            int qDati = 10000;

            for (int i = 0; i < qDati; i++)
            {
                Random rnd = new Random();

                PlayerHp = rnd.Next(1, 30);
                PlayerMana = rnd.Next(1, 30);
                PlayerAttack = rnd.Next(1, 30);
                PlayerIntelligence = rnd.Next(1, 30);
                PlayerMagicCost = rnd.Next(1, 30);

                EnemyHp = rnd.Next(1, 30);
                EnemyMana = rnd.Next(1, 30);
                EnemyAttack = rnd.Next(1, 30);
                EnemyIntelligence = rnd.Next(1, 30);
                HasManaPotion = rnd.Next(0, 101) >= 50 ? 0 : 1;
                HasHpPotion = rnd.Next(0, 101) >= 50 ? 0 : 1;
                EnemyMagicHealCost = rnd.Next(1, 10);
                EnemyMagicDamageCost = rnd.Next(1, 10);

                if (EnemyHp <= PlayerAttack || (EnemyHp <= PlayerIntelligence && PlayerMana>=PlayerMagicCost))
                {
                    if (HasHpPotion==1)
                        WriteData(3);
                    else if (EnemyMana >= EnemyMagicHealCost)
                        WriteData(2);
                }

                if (PlayerHp <= EnemyAttack)
                    WriteData(0);

                if (PlayerHp <= EnemyIntelligence && EnemyMana >= EnemyMagicDamageCost)
                    WriteData(1);

                if (EnemyMana >= EnemyMagicDamageCost)
                    WriteData(1);

                if (EnemyHp > PlayerAttack && (EnemyHp > PlayerIntelligence && PlayerMana >= PlayerMagicCost || EnemyHp <= PlayerIntelligence && PlayerMana < PlayerMagicCost))
                    WriteData(4);

                WriteData(1);

                Console.WriteLine(100 * i / qDati + "%");

            }


        }

        /// <summary>
        /// 0)Attacco normale
        /// 1)Attacco magico(5 mana)
        /// 2)Cura(5 mana)
        /// 3)PozioneHp
        /// 4)PozioneMana
        /// </summary>
        /// <param name="output"></param>
        static void WriteData(int output)
        {
            using (StreamWriter sw = new StreamWriter("Data.txt",true))
            {
                sw.WriteLine(PlayerHp + "," + PlayerMana + "," + PlayerAttack + "," + PlayerIntelligence + "," + PlayerMagicCost + "," + EnemyHp + "," + EnemyMana + "," + EnemyAttack + "," + EnemyIntelligence + "," + HasManaPotion + "," + HasHpPotion + "," + EnemyMagicHealCost + "," + EnemyMagicDamageCost + "," + output);
            }
        }

    }
}
