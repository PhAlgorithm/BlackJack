using System;

namespace Game
{
    class Program
    {
        const int JAC21 = 21;   //введем константы для правил, если правила поменяются
        const int JAC22 = 22;   // меняем только здесь
        const int JAC19 = 19;

        static void GreatCards(Card[] array)
        {
            int index = 0;
            foreach (var i in Enum.GetValues(typeof(Suit)))
            {
                foreach (var j in Enum.GetValues(typeof(Face)))
                {
                    array[index] = new Card() { Suit = (Suit)i, Face = (Face)j };
                    index++;
                }
            }
        }

        static void MixCards(Card[] array)
        {
            Random randome = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                int ran = randome.Next(0, array.Length);

                Card temp = array[i];
                array[i] = array[ran];
                array[ran] = temp;
            }
        }

        static void ShowCards(Card[] array)
        {
            foreach (Card item in array)
            {
                Console.WriteLine(item);
            }
        }

        static void DaelCards(Card[] arrayMain
            , Card[] array
            , Card[] array2
            , ref Card[] arrayMain2)
        {
            int j = 0; int f = 0;
            for (int i = 0, n = arrayMain.Length; i < 4; i++, n--)
            {
                if (i % 2 == 0)
                {
                    array[j] = arrayMain[n - 1];
                    j++;
                }
                else
                {
                    array2[f] = arrayMain[n - 1];
                    f++;
                }
            }

            arrayMain2 = new Card[arrayMain.Length - 4];
            Array.Copy(arrayMain, arrayMain2, arrayMain.Length - 4);
        }

        static int CountCardsValue(Card[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += (int)array[i].Face;
            }

            return sum;
        }

        static int AddCount(Card[] arrayMain
            , Card[] array
            , ref Card[] arrayMod
            , ref Card[] arrayMainMod)
        {
            int sum = 0;
            arrayMod = new Card[array.Length + 1];

            for (int j = 0; j < array.Length; j++)
            {
                arrayMod[j] = array[j];
                sum += (int)array[j].Face;
            }

            for (int i = array.Length; i < arrayMod.Length; i++)
            {
                arrayMod[i] = arrayMain[arrayMain.Length - 1];
                sum += (int)arrayMod[i].Face;
            }

            arrayMainMod = new Card[arrayMain.Length - 1];
            Array.Copy(arrayMain, arrayMainMod, arrayMain.Length - 1);
            return sum;
        }

        static void CheckBeforCount(int countDeal
            , int countPlay
            , ref int winDeal
            , ref int winPlay
            , ref bool flagForFirstChek)
        {
            if (countDeal == countPlay & (countDeal == JAC21 & countDeal == JAC22))
            {
                Console.WriteLine("Eggs");
                flagForFirstChek = false;
            }

            else if ((countDeal == JAC21 | countDeal == JAC22)
                   & (countPlay != JAC21 | countPlay != JAC22))
            {
                Console.WriteLine("Dealler win");
                winDeal++;
                flagForFirstChek = false;
            }

            else if ((countDeal != JAC21 | countDeal != JAC22)
                   & (countPlay == JAC21 | countPlay == JAC22))
            {
                Console.WriteLine("You win");
                winPlay++;
                flagForFirstChek = false;
            }
        }

        static void CheckAfterCount(int countDeal
            , int countPlay
            , ref int winDeal
            , ref int winPlay)
        {
            if (countDeal == countPlay)
            {
                Console.WriteLine("Eggs :)");
            }

            else if (countDeal == JAC21 & countPlay != JAC21)
            {
                Console.WriteLine("Dealler win");
                winDeal++;
            }

            else if (countPlay == JAC21 & countDeal != JAC21)
            {
                Console.WriteLine("You win");
                winPlay++;
            }

            else if (countDeal < JAC21 & countPlay > JAC21)
            {
                Console.WriteLine("Dealler win");
                winDeal++;
            }
            else if (countDeal > JAC21 & countPlay < JAC21)
            {
                Console.WriteLine("You win");
                winPlay++;
            }
            else if ((countDeal > JAC21 & countPlay > JAC21))
            {
                if (countDeal < countPlay)
                {
                    Console.WriteLine("Dealler win");
                    winDeal++;
                }
                else
                {
                    Console.WriteLine("You win");
                    winPlay++;
                }
            }
            else if ((countDeal < JAC21 & countPlay < JAC21))
            {
                if (countDeal > countPlay)
                {
                    Console.WriteLine("Dealler win");
                    winDeal++;
                }
                else
                {
                    Console.WriteLine("You win");
                    winPlay++;
                }
            }
        }

        static void Main(string[] args)
        {
            int dealerWin = 1;
            int playerWin = 1;

            bool askForExitGame = false;

            do
            {
                int f = Enum.GetNames(typeof(Face)).Length;  //ведем размер массива, зависящий от кол-ва
                int c = Enum.GetNames(typeof(Suit)).Length;  // элементов в структурах

                Card[] Cards = new Card[f * c];

                GreatCards(Cards);

                MixCards(Cards);

                Card[] Player = new Card[2];
                Card[] Dealer = new Card[2];

                bool playing = true;

                bool flagForFirstChek = true;

                do
                {
                    Console.Write("Would you like to Hit (H) or Stay (S)?: ");

                    string answer = Console.ReadLine();

                    Console.Clear();

                    switch (answer.ToUpper())
                    {
                        #region//H
                        case "H":

                            Console.WriteLine("You are Hit");

                            DaelCards(Cards, Dealer, Player, ref Cards);
                            ShowCards(Player);

                            Console.WriteLine($"You have {CountCardsValue(Player)} point");

                            CheckBeforCount(
                                CountCardsValue(Dealer)
                                , CountCardsValue(Player)
                                , ref dealerWin
                                , ref playerWin
                                , ref flagForFirstChek);

                            if (!flagForFirstChek)
                            {
                                break;
                            }
                            else
                            {
                                while (CountCardsValue(Dealer) < JAC19)
                                {
                                    AddCount(Cards, Dealer, ref Dealer, ref Cards);
                                    CountCardsValue(Dealer);
                                }

                                bool flagForLocalChek = true;

                                do
                                {
                                    Console.Write("Would you like to get card Yes (Y)/ No (N) ?: ");

                                    string answerForPlayer = Console.ReadLine();
                                    switch (answerForPlayer.ToUpper())
                                    {
                                        case "Y":
                                            Console.Clear();
                                            AddCount(Cards, Player, ref Player, ref Cards);
                                            ShowCards(Player);
                                            Console.WriteLine($"You have {CountCardsValue(Player)} point");
                                            break;

                                        case "N":
                                            flagForLocalChek = false;
                                            Console.Clear();
                                            break;

                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Invalid Input");
                                            flagForLocalChek = true;
                                            break;
                                    }
                                }
                                while (flagForLocalChek);

                                Console.WriteLine($"Dealer has {CountCardsValue(Dealer)} point");
                                Console.WriteLine($"You have {CountCardsValue(Player)} point");

                                break;
                            }
                        #endregion

                        #region//S
                        case "S":
                            Console.WriteLine("You are Stay");

                            DaelCards(Cards, Player, Dealer, ref Cards);
                            ShowCards(Player);

                            CheckBeforCount(CountCardsValue(Dealer)
                                , CountCardsValue(Player)
                                , ref dealerWin
                                , ref playerWin
                                , ref flagForFirstChek);

                            Console.WriteLine($"You have {CountCardsValue(Player)} point");

                            if (!flagForFirstChek)
                            {
                                break;
                            }
                            else
                            {
                                bool flagForLocalChek1 = true;

                                do
                                {
                                    Console.Write("Would you like to get card Yes (Y)/ No (N) ?: ");

                                    string answerForPlayer = Console.ReadLine();
                                    switch (answerForPlayer.ToUpper())
                                    {
                                        case "Y":
                                            Console.Clear();
                                            AddCount(Cards, Player, ref Player, ref Cards);
                                            ShowCards(Player);
                                            Console.WriteLine($"You have {CountCardsValue(Player)} point");
                                            break;

                                        case "N":
                                            flagForLocalChek1 = false;
                                            Console.Clear();
                                            break;

                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Invalid Input");

                                            flagForLocalChek1 = true;
                                            break;
                                    }
                                }
                                while (flagForLocalChek1);

                                while (CountCardsValue(Dealer) < JAC19)
                                {
                                    AddCount(Cards, Dealer, ref Dealer, ref Cards);
                                    CountCardsValue(Dealer);
                                }

                                Console.WriteLine($"Dealer has {CountCardsValue(Dealer)} point");
                                Console.WriteLine($"You have {CountCardsValue(Player)} point");

                                break;
                            }
                        #endregion

                        default:
                            Console.WriteLine("Invalid Input");
                            playing = false;
                            break;
                    }

                }
                while (!playing);

                if (flagForFirstChek)
                {
                    CheckAfterCount(CountCardsValue(Dealer)
                        , CountCardsValue(Player)
                        , ref dealerWin
                        , ref playerWin);
                }
                
                Console.Write("Would you like to take game again Yes (Y)/ No (N) ?:  ");

                string answerForGame = Console.ReadLine();

                switch (answerForGame.ToUpper())
                {
                    case "Y":
                        Console.Clear();
                        askForExitGame = true;
                        break;

                    case "N":
                        Console.Clear();
                        Console.WriteLine($"Total score:  Dealer {dealerWin - 1} : Plaer {playerWin - 1}");

                        askForExitGame = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        askForExitGame = true;
                        break;
                }
            }
            while (askForExitGame);

            Console.ReadKey();
        }
    }
}
