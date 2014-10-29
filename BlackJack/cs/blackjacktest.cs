using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using System.Collections.Generic;

namespace GamesTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void GetHandTotalTest()
        {
            Player p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, 9));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(21, p.GetHandTotal());
            bool pure = true;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);


            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, 5));
            p.AddCard(new BlackJackCard(Suite.Clubs, 7));
            p.AddCard(new BlackJackCard(Suite.Clubs, 9));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = true;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 6));
            Assert.AreEqual(17, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 7));
            Assert.AreEqual(18, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 8));
            Assert.AreEqual(19, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 9));
            Assert.AreEqual(20, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsTrue(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 11));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsTrue(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.King));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsTrue(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Queen));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsTrue(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Jack));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsTrue(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 11));
            Assert.AreEqual(12, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(12, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(14, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(11, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, 4));
            p.AddCard(new BlackJackCard(Suite.Clubs, 8));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(14, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(16, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(17, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(18, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(19, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(20, p.GetHandTotal());

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(21, p.GetHandTotal());
            pure = true;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            p = new Player(1, 2, null);
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, Card.Ace));
            Assert.AreEqual(12, p.GetHandTotal());
            pure = true;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);
        }
        

        [TestMethod]
        public void CanDoubleDownTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void HasSplitTest()
        {
            var p = new Player(0, 500, null);
            p.AddCard(new BlackJackCard(Suite.Hearts, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            Assert.IsFalse(p.HasSplit());

            p = new Player(0, 500, null);
            p.AddCard(new BlackJackCard(Suite.Hearts, 10));
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            Assert.IsTrue(p.HasSplit());
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            Assert.IsFalse(p.HasSplit());
        }

        [TestMethod]
        public void HasBlackJackTest()
        {
            //BlackJack scenario
            var p = new Player(0, 500, null);
            p.AddCard(new BlackJackCard(Suite.Hearts, Card.Ace));
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            bool pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsTrue(pure);

            p = new Player(0, 500, null);
            p.AddCard(new BlackJackCard(Suite.Hearts, 9));
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            pure = false;
            Assert.IsFalse(p.HasBlackJack(out pure));
            p.AddCard(new BlackJackCard(Suite.Clubs, 2));
            pure = false;
            Assert.IsTrue(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            //Non-BlackJack scenario
            p = new Player(0, 500, null);
            p.AddCard(new BlackJackCard(Suite.Hearts, 10));
            p.AddCard(new BlackJackCard(Suite.Clubs, 10));
            pure = false;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);
            p.AddCard(new BlackJackCard(Suite.Clubs, 2));
            pure = false;
            Assert.IsFalse(p.HasBlackJack(out pure));
            Assert.IsFalse(pure);

            //Split scenario

            //DoubleDown scenario

        }
    }

    [TestClass]
    public class BlackJackGameTest
    {
        private Random randGen = new Random();
        
        [TestMethod]
        public void MakeBetsTest()
        {
            Table table = new RedTable(PayOut.ThreeTwo);
            Console.WriteLine("Table minimum bet: " + table.MinimumBet);
            var cash = randGen.Next(0, (int)Chip.Purple + 1);
            Console.WriteLine("Player cash: " + cash);
            var numPlayers = randGen.Next(1, 11);
            Console.WriteLine("Number of players: " + numPlayers);
            Dictionary<int,Player> players = new Dictionary<int, Player>();
            for (var i = 0; i < numPlayers; i++)
            {
                players.Add(i, new Player(i, cash, table));
            }
            BlackJackGame.MakeBets(table, players);
            var leftOver = cash - table.MinimumBet;
            Console.WriteLine("Calculated leftover cash: " + leftOver);
            for (var i = 0; i < numPlayers; i++)
            {
                var actual = table.GetBetAmount(i);
                Console.WriteLine("Player {0} bet amount: {1}", i, actual);
                Assert.AreEqual(table.MinimumBet, actual);
                var rem = players[i].Chips.GetTotal();
                Console.WriteLine("Remaining player cash: " + rem);
                Assert.AreEqual(leftOver, rem);
            }
        }

        [TestMethod]
        public void CreateDeckTest()
        {
            var deck = BlackJackGame.CreateDeck();
            Assert.IsNotNull(deck);
            Assert.AreEqual(52, deck.Length);
            Dictionary<Suite, Dictionary<int, int>> dict = new Dictionary<Suite, Dictionary<int, int>>();
            foreach (var card in deck)
            {
                Dictionary<int, int> hash;
                if (dict.ContainsKey(card.Suite))
                {
                    hash = dict[card.Suite];
                }
                else
                {
                    hash = new Dictionary<int, int>();
                    dict.Add(card.Suite, hash);
                }

                hash.Add(card.Value, 0);
            }

            var suites = Enum.GetValues(typeof(Suite));
            Assert.AreEqual(suites.Length, dict.Keys.Count);
            foreach (Suite s in suites)
            {
                Assert.IsTrue(dict.ContainsKey(s));
                var vals = dict[s];
                Assert.AreEqual(13, vals.Count);

                Dictionary<int, int> unique = new Dictionary<int, int>();
                foreach (var val in vals.Keys)
                {
                    Assert.IsTrue(val >= 1 && val <= Card.Ace);
                    unique.Add(val, 0);
                }
                dict.Remove(s);
            }

            Assert.AreEqual(0, dict.Count);

            var numDecks = randGen.Next(1, 11);
            deck = BlackJackGame.CreateDeck(numDecks);
            Assert.AreEqual(numDecks * 52, deck.Length);

            for (var i = 0; i < numDecks; i++)
            {
                dict = new Dictionary<Suite, Dictionary<int, int>>();
                var end = (i + 1) * 52;
                for (var j = i * 52; j < end; j++)
                {
                    var card = deck[j];
                    Dictionary<int, int> hash;
                    if (dict.ContainsKey(card.Suite))
                    {
                        hash = dict[card.Suite];
                    }
                    else
                    {
                        hash = new Dictionary<int, int>();
                        dict.Add(card.Suite, hash);
                    }

                    hash.Add(card.Value, 0);
                }

                suites = Enum.GetValues(typeof(Suite));
                Assert.AreEqual(suites.Length, dict.Keys.Count);
                foreach (Suite s in suites)
                {
                    Assert.IsTrue(dict.ContainsKey(s));
                    var vals = dict[s];
                    Assert.AreEqual(13, vals.Count);

                    Dictionary<int, int> unique = new Dictionary<int, int>();
                    foreach (var val in vals.Keys)
                    {
                        Assert.IsTrue(val >= 1 && val <= Card.Ace);
                        unique.Add(val, 0);
                    }
                    dict.Remove(s);
                }

                Assert.AreEqual(0, dict.Count);
            }
        }

        [TestMethod]
        public void CreatePlayersTest()
        {
            var c = randGen.Next(1, 10);
            var p = BlackJackGame.CreatePlayers(null,c, 250);
            Assert.IsNotNull(p);
            Assert.AreEqual(c, p.Count);
            foreach (var i in p.Values)
            {
                Assert.AreEqual(4, i.Chips.Count);
                Assert.AreEqual(Chip.Green, i.Chips.Pop());
                Assert.AreEqual(Chip.Green, i.Chips.Pop());
                Assert.AreEqual(Chip.Black, i.Chips.Pop());
                Assert.AreEqual(Chip.Black, i.Chips.Pop());
            }
        }

        [TestMethod]
        public void CreateTableTest()
        {
            var game = new BlackJackGame();

            int[] choices = (int[])Enum.GetValues(typeof(TableBet));
            var str = BlackJackGame.CreateChoicesString(choices);

            //Default
            bool valid = false;
            int defVal = choices[0];
            var table = game.CreateTable(string.Empty, choices, str, out valid);
            Assert.IsTrue(valid);
            Assert.IsNotNull(table);
            Assert.AreEqual(defVal, table.MinimumBet);

            /*
            //Default number of players
            valid = false;
            numPlayers = game.CreateTable("        ",defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(defVal, numPlayers);

            //Valid
            Random randGen = new Random();
            valid = false;            
            numPlayers = game.CreateTable("   " + 1 + "     ",randGen.Next(2), out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(1, numPlayers);

            //Valid            
            var r1 = randGen.Next(2,int.MaxValue);
            valid = false;
            numPlayers = game.CreateTable("   " + r1 + "     ",defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(r1, numPlayers);

            //Valid
            valid = false;
            numPlayers = game.CreateTable(r1.ToString(),defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(r1, numPlayers);

            //Valid
            valid = false;
            numPlayers = game.CreateTable("\t" + int.MaxValue + "      \t ",defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(int.MaxValue, numPlayers);

            //Error
            valid = false;
            numPlayers = game.CreateTable("  " + r1 + "  d    ",defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.CreateTable("    7.1    ",defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.CreateTable("    0    ",defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.CreateTable("    " + int.MinValue + "    ",defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.CreateTable("  " + randGen.Next(int.MinValue + 1, 1) + "   ",defVal, out valid);
            Assert.IsFalse(valid); */
        }

        [TestMethod]
        public void GetNumPlayersTest()
        {
            var game = new BlackJackGame();

            //Default number of players
            bool valid = false;
            int defVal = 1;
            var numPlayers = game.GetNumPlayers(string.Empty, defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(1, numPlayers);

            //Default number of players
            valid = false;
            numPlayers = game.GetNumPlayers("        ", defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(defVal, numPlayers);

            //Valid
            Random randGen = new Random();
            valid = false;
            numPlayers = game.GetNumPlayers("   " + 1 + "     ", randGen.Next(2), out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(1, numPlayers);

            //Valid            
            var r1 = randGen.Next(2, int.MaxValue);
            valid = false;
            numPlayers = game.GetNumPlayers("   " + r1 + "     ", defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(r1, numPlayers);

            //Valid
            valid = false;
            numPlayers = game.GetNumPlayers(r1.ToString(), defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(r1, numPlayers);

            //Valid
            valid = false;
            numPlayers = game.GetNumPlayers("\t" + int.MaxValue + "      \t ", defVal, out valid);
            Assert.IsTrue(valid);
            Assert.AreEqual(int.MaxValue, numPlayers);

            //Error
            valid = false;
            numPlayers = game.GetNumPlayers("  " + r1 + "  d    ", defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.GetNumPlayers("    7.1    ", defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.GetNumPlayers("    0    ", defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.GetNumPlayers("    " + int.MinValue + "    ", defVal, out valid);
            Assert.IsFalse(valid);

            //Error
            valid = false;
            numPlayers = game.GetNumPlayers("  " + randGen.Next(int.MinValue + 1, 1) + "   ", defVal, out valid);
            Assert.IsFalse(valid);
        }
    }

    [TestClass]
    public class ChipStackTest
    {
        private Random randGen = new Random();
        
        [TestMethod]
        public void RemoveAmountTest()
        {
            var s = new ChipStack();
            s.Push(Chip.Purple);

            var s2 = s.RemoveAmount(500);
            Assert.AreEqual(0, s.Count);
            Assert.AreEqual(1, s2.Count);
            Assert.AreEqual(Chip.Purple, s2.Pop());
            
            bool exThrown = false;
            try
            {
                s2 = s.RemoveAmount(506);
            }
            catch (Exception)
            {
                exThrown = true;
            }
            Assert.IsTrue(exThrown);

            s.Push(Chip.Purple);
            var unchanged = s.Count;
            s2 = s.RemoveAmount(506);
            Assert.AreEqual(0, s2.Count);
            Assert.AreEqual(unchanged, s.Count);

            //s.Push(Chip.Purple);
            for (int i = 0; i < 6; i++)
            {
                s.Push(Chip.White);
            }

            s2 = s.RemoveAmount(506);
            Assert.AreEqual(0, s.Count);
            Assert.AreEqual(7, s2.Count);
            for (int i = 0; i < 6; i++)
            {
                Assert.AreEqual(Chip.White, s2.Pop());
            }
            Assert.AreEqual(Chip.Purple, s2.Pop());
            Assert.AreEqual(0, s.Count);

            s.Push(Chip.Purple);
            s.Push(Chip.Red);
            s.Push(Chip.Green);
            s2 = s.RemoveAmount(506);
            Assert.AreEqual(3, s2.Count);
            Assert.AreEqual(Chip.White, s2.Pop());
            Assert.AreEqual(Chip.Red, s2.Pop());
            Assert.AreEqual(Chip.Purple, s2.Pop());            
            Assert.AreEqual(8, s.Count);

            for (var i = 0; i < 4; i++)
            {
                Assert.AreEqual(Chip.White, s.Pop());
            }

            for (var i = 0; i < 4; i++)
            {
                Assert.AreEqual(Chip.Red, s.Pop());
            }
            /*
            Test Name:	MakeBetsTest
Test Outcome:	Failed
Result Message:	Assert.AreEqual failed. Expected:<5>. Actual:<4>.
Result StandardOutput:	
Table minimum bet: 5
Player cash: 204
Number of players: 2
Calculated leftover cash: 199
Player 0 bet ammount: 4
            */

            var cashStack = Cashier.Exchange(204);
            var removeStack = cashStack.RemoveAmount(5);
            var leftOverCash = 199;
            Assert.AreEqual(leftOverCash, cashStack.GetTotal());

            /*
            Test Name:	MakeBetsTest
Test Outcome:	Failed
Result Message:	Assert.AreEqual failed. Expected:<5>. Actual:<0>.
Result StandardOutput:	
Table minimum bet: 5
Player cash: 175
Number of players: 9
Calculated leftover cash: 170
Player 0 bet ammount: 0
*/
            cashStack = Cashier.Exchange(175);
            removeStack = cashStack.RemoveAmount(5);
            leftOverCash = 170;
            Assert.AreEqual(leftOverCash, cashStack.GetTotal());
        }

        [TestMethod]
        public void GetChipsTest()
        {
            var s = ChipStack.GetChips((int)Chip.Purple);
            Assert.AreEqual(Chip.Purple, s.Pop());

            s = ChipStack.GetChips((int)Chip.Purple + (int)Chip.Red);
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Purple, s.Pop());

            s = ChipStack.GetChips((int)Chip.Purple + (int)Chip.Purple);
            Assert.AreEqual(Chip.Purple, s.Pop());
            Assert.AreEqual(Chip.Purple, s.Pop());

            s = ChipStack.GetChips((int)Chip.Green + (int)Chip.Red + (int)Chip.Red + (int)Chip.White + 0.2);
            Assert.AreEqual(Chip.White, s.Pop());
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Green, s.Pop());
        }

        [TestMethod]
        public void PopTest()
        {
            var s = new ChipStack();
            s.Push(Chip.Green);
            s.Push(Chip.White);
            s.Push(Chip.Red);
            

            Assert.AreEqual(Chip.White, s.Pop());
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Green, s.Pop());
        }

        [TestMethod]
        public void PushTest()
        {
            var chipValues = (Chip[])Enum.GetValues(typeof(Chip));
            var chip = chipValues[randGen.Next(0, chipValues.Length)];
            var s = new ChipStack();
            s.Push(chip);
            Assert.AreEqual(1,s.Count);
            Assert.AreEqual(chip,s.Peek());
            Assert.AreEqual(chip,s.Pop());
            Assert.AreEqual(0,s.Count);

            var myChips = new Chip[] { Chip.Purple, Chip.Red, Chip.Green };
            s = new ChipStack();
            foreach (var thisChip in myChips)
            {
                s.Push(thisChip);
            }
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Green, s.Pop());
            Assert.AreEqual(Chip.Purple, s.Pop());

            myChips = new Chip[] { Chip.Purple, Chip.Purple, Chip.Green };
            s = new ChipStack();
            foreach (var thisChip in myChips)
            {
                s.Push(thisChip);
            }
            Assert.AreEqual(Chip.Green, s.Pop());
            Assert.AreEqual(Chip.Purple, s.Pop());
            Assert.AreEqual(Chip.Purple, s.Pop());

            myChips = new Chip[] { Chip.Green, Chip.Green, Chip.Green };
            s = new ChipStack();
            foreach (var thisChip in myChips)
            {
                s.Push(thisChip);
            }
            Assert.AreEqual(Chip.Green, s.Pop());
            Assert.AreEqual(Chip.Green, s.Pop());
            Assert.AreEqual(Chip.Green, s.Pop());

            myChips = new Chip[] { Chip.Black, Chip.Red};
            s = new ChipStack();
            foreach (var thisChip in myChips)
            {
                s.Push(thisChip);
            }
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Black, s.Pop());

            myChips = new Chip[] { Chip.Green, Chip.Red, Chip.Purple };
            s = new ChipStack();
            foreach (var thisChip in myChips)
            {
                s.Push(thisChip);
            }
            Assert.AreEqual(Chip.Red, s.Pop());
            Assert.AreEqual(Chip.Green, s.Pop());
            Assert.AreEqual(Chip.Purple, s.Pop());

            s = new ChipStack();
            for (int i = 0; i < 3; i++)
            {
                chip = chipValues[randGen.Next(0, chipValues.Length)];
                s.Push(chip);
                Console.WriteLine("Inserted: {0}", chip);
            }

            Chip prev = s.Pop();
            while(s.Count>0)
            {                
                var current = s.Pop();
                Console.WriteLine("Prev: {0}", prev);
                Console.WriteLine("Current: {0}", current);
                Assert.IsTrue(prev == current || prev < current);
                prev = current;
            }
        }
    }

    [TestClass]
    public class BlackJackCardTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var suites = (Suite[])Enum.GetValues(typeof(Suite));
            var r1 = new Random().Next(1, 11);
            var s = suites[new Random().Next(0, suites.Length)];
            Card c = new BlackJackCard(s, r1);
            Assert.AreEqual(s, c.Suite);
            Assert.AreEqual(r1, c.Value);

            bool exThrown = false;
            try
            {
                c = new BlackJackCard(s, new Random().Next(15, int.MaxValue));
            }
            catch (Exception)
            {
                exThrown = true;
            }

            Assert.IsTrue(exThrown);
        }

        [TestMethod]
        public void ActualValueTest()
        {
            var randGen = new Random();
            var suites = (Suite[])Enum.GetValues(typeof(Suite));
            var r1 = randGen.Next(1, 11);
            var r2 = randGen.Next(1, 11);
            while (r1 == r2)
            {
               r2 = randGen.Next(1, 11);
            }
            Assert.AreNotEqual(r1, r2);
            var s = suites[randGen.Next(0, suites.Length)];

            //Change value
            BlackJackCard c = new BlackJackCard(s, r1);
            Assert.AreEqual(r1, c.ActualValue);
            c.Value = r2;
            Assert.AreEqual(r2, c.ActualValue);

            //Verify Face cards all return 10, Ace returns 11
            var faceCards = new List<int>() { Card.Jack, Card.Queen, Card.King };
            foreach (var face in faceCards)
            {
                c = new BlackJackCard(s, face);
                Assert.AreEqual(10, c.ActualValue);
            }

            c = new BlackJackCard(s, Card.Ace);
            Assert.AreEqual(11, c.ActualValue);


            bool exThrown = false;
            try
            {
                c.Value = randGen.Next(15, int.MaxValue);
            }
            catch (Exception)
            {
                exThrown = true;
            }

            Assert.IsTrue(exThrown);
        }
    }

    [TestClass]
    public class CardTest
    {        
        [TestMethod]
        public void ConstructorTest() 
        {
            var suites = (Suite[])Enum.GetValues(typeof(Suite));            
            var r1 = new Random().Next(1, 15);
            var s = suites[new Random().Next(0,suites.Length)];
            Card c = new Card(s, r1);
            Assert.AreEqual(s, c.Suite);
            Assert.AreEqual(r1, c.Value);

            bool exThrown = false;
            try
            {
                c = new Card(s, new Random().Next(15, int.MaxValue));
            }
            catch(Exception)
            {
                exThrown = true;
            }

            Assert.IsTrue(exThrown);
        }

        [TestMethod]
        public void SuiteTest()
        {
            var randGen = new Random();
            var suites = (Suite[])Enum.GetValues(typeof(Suite));
            var r1 = randGen.Next(1, 15);
            var s = suites[randGen.Next(0, suites.Length)];
            var s2 = suites[randGen.Next(0, suites.Length)];

            while (s == s2)
            {
                s2 = suites[randGen.Next(0, suites.Length)];
            } 
            Assert.AreNotEqual(s, s2);
            Card c = new Card(s, r1);            
            Assert.AreEqual(s, c.Suite);
            c.Suite = s2;
            Assert.AreEqual(s2, c.Suite);
        }

        [TestMethod]
        public void ValueTest()
        {
            var randGen = new Random();
            var suites = (Suite[])Enum.GetValues(typeof(Suite));
            var r1 = randGen.Next(1, 15);
            var r2 = randGen.Next(1, 15);
            while (r1 == r2)
            {
                r2 = randGen.Next(1, 15); 
            } 
            Assert.AreNotEqual(r1, r2);
            var s = suites[randGen.Next(0, suites.Length)];
            
            Card c = new Card(s, r1);
            Assert.AreEqual(r1, c.Value);
            c.Value = r2;
            Assert.AreEqual(r2, c.Value);

            bool exThrown = false;
            try
            {
                c.Value = randGen.Next(15, int.MaxValue);
            }
            catch(Exception)
            {
                exThrown = true;
            }

            Assert.IsTrue(exThrown);
        }

        [TestMethod]
        public void GetValueTest()
        {
            var suites = (Suite[])Enum.GetValues(typeof(Suite));
            var r1 = new Random().Next(1, 11);
            var s = suites[new Random().Next(0, suites.Length)];
            Card c = new Card(s, r1);
            Assert.AreEqual(r1.ToString(), c.GetFaceValue());

            c.Value = 11;
            Assert.AreEqual("Jack", c.GetFaceValue());

            c.Value = 12;
            Assert.AreEqual("Queen", c.GetFaceValue());

            c.Value = 13;
            Assert.AreEqual("King", c.GetFaceValue());
        }

        [TestMethod]
        public void JackTest()
        {
            var cardVal = 10;
            Assert.AreEqual(cardVal + 1, Card.Jack);
        }

        [TestMethod]
        public void QueenTest()
        {
            var cardVal = 10;
            Assert.AreEqual(cardVal + 2, Card.Queen);
        }

        [TestMethod]
        public void KingTest()
        {
            var cardVal = 10;
            Assert.AreEqual(cardVal + 3, Card.King);
        }

        [TestMethod]
        public void AceTest()
        {
            var cardVal = 10;
            Assert.AreEqual(cardVal + 4, Card.Ace);
        }
    }

    /*
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void PushTest()
        {
            Stack<int> a = new Stack<int>();
            var val = new Random().Next();
            a.Push(val);
            Assert.AreEqual(val, a.Pop());
        }

        [TestMethod]
        public void PopTest()
        {
            Stack<int> s = new Stack<int>();
            var randGen = new Random();
            var r1 = randGen.Next();
            var r2 = randGen.Next();
            var r3 = randGen.Next();
            s.Push(r1);
            s.Push(r2);
            s.Push(r3);
            Assert.IsFalse(s.IsEmpty);
            var v1 = s.Pop();
            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(r3, v1);
            Assert.IsFalse(s.IsEmpty);
            var v2 = s.Pop();
            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(r2, v2);
            var v3 = s.Pop();
            Assert.AreEqual(r1, v3);
            Assert.IsTrue(s.IsEmpty);
        }

        [TestMethod]
        public void PeekTest()
        {
            Stack<int> s = new Stack<int>();
            var randGen = new Random();
            var r1 = randGen.Next();
            var r2 = randGen.Next();
            var r3 = randGen.Next();
            s.Push(r1);
            s.Push(r2);
            s.Push(r3);
            Assert.IsFalse(s.IsEmpty);

            //Peek then Pop
            var p1 = s.Peek();
            Assert.AreEqual(r3, p1);
            s.Pop();

            //Peek then Pop
            var p2 = s.Peek();
            Assert.AreEqual(r2, p2);
            Assert.IsFalse(s.IsEmpty);
            s.Pop();

            //Peek then Pop, then Peek
            var p3 = s.Peek();
            Assert.AreEqual(r1, p3);
            Assert.IsFalse(s.IsEmpty);
            s.Pop();
            Assert.IsTrue(s.IsEmpty);

            bool exThrown = false;
            try
            {
                s.Peek();
            }
            catch (Exception)
            {
                exThrown = true;
            }

            Assert.IsTrue(exThrown);
        }

        [TestMethod]
        public void IsEmptyTest()
        {
            Stack<int> s = new Stack<int>();
            Assert.IsTrue(s.IsEmpty);

            s.Push(new Random().Next());
            Assert.IsFalse(s.IsEmpty);

            s.Pop();
            Assert.IsTrue(s.IsEmpty);
        }
    }*/
}