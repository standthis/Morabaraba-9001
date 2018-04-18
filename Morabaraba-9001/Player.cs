﻿using System;
using System.Collections.Generic;
namespace Morabaraba_9001
{
    public class Player : IPlayer
    {
        public Player(string name, Color color,IBoard board)
        {
            Name = name;
            Color = color;
            State = PlayerState.Placing;
            Cows = new List<ICow>();
            Board = board;
            for (int i = 0; i < 12; i++)
            {
                Cows.Add(new Cow(Color));
            }
        }

        public Player(PlayerState state, Cow cow,IBoard board)//for testing purposes, instantiate a player with a given cow in it's cow list
        {
            State = state;
            Cows = new List<ICow>();
            Cows.Add(cow);
            Board = board;
            Board.AllTiles[cow.pos].Occupied = true;
            for (int i = 1; i < 12; i++)
            {
                Cows.Add(new Cow(Color));
            }
        }


        public List<ICow> Cows { get; private set; }
        public string Name { get; }

        public Color Color { get; }

        public PlayerState State { get; private set; }
        public IBoard Board { get; private set; }

        public (char, int) GetMove(string what)
        {
            Console.WriteLine(what);
            Console.Write("Row: ");
            char rowInput = Console.ReadKey().KeyChar;
            bool is_char = Char.TryParse(rowInput + "", out rowInput);
            Console.WriteLine();
            Console.Write("Column: ");
            char getCol = Console.ReadKey().KeyChar;
            Console.WriteLine();
            int col;
            bool is_numeric = System.Int32.TryParse(getCol + "", out col);
            if (is_numeric && is_char)
            {
                return (Char.ToUpper(rowInput), col);

            }
            else
            {
                Console.WriteLine("Row requires a character and Col requires a number.Please enter valid input ");
                return GetMove(what);
            }
        }

        public MoveError placeCow((char, int) toPos, IReferee referee)
        {
            MoveError error = referee.Place(this, toPos);
            if (error != MoveError.Valid)
            {
                return error;
            }
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].status == cowStatus.Unplaced)
                {
                    Cows[i].pos = toPos;
                    Cows[i].status = cowStatus.Placed;
                    Board.AllTiles[toPos].Occupied = true;
                    break;
                }
            }
            return MoveError.Valid;
        }

        public MoveError moveCow((char, int) fromPos, (char, int) toPos, IReferee referee)
        {

            if (State == PlayerState.Moving)
            {
                MoveError error = referee.Move(this, fromPos, toPos);
                if (error != MoveError.Valid)
                {
                    return error;
                }
            }
            if(State == PlayerState.Flying){
                MoveError error = referee.Fly(this, fromPos, toPos);
                if(error != MoveError.Valid){
                    return error;
                }
            }
           
            for (int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(fromPos))
                {
                    Cows[i].pos = toPos;
                    Board.AllTiles[toPos].Occupied = true;
                    Board.AllTiles[fromPos].Occupied = false;
                    break;
                }
            }
            return MoveError.Valid;
        }

        public bool hasCowAtPos((char, int) pos)
        {
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(pos) && Cows[i].status.Equals(cowStatus.Placed))
                {
                    return true;
                }
            }
            return false;
        }
        
        public int numCowsOnBoard()
        {
            int count = 0;
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].status.Equals(cowStatus.Placed)) count++;
            }
            return count;
        }

        public MoveError killCow((char, int) pos,IReferee referee)
        {
            MoveError error = referee.KillCow(this, pos);
            if (error != MoveError.Valid)
                return error;
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(pos))
                {
                    Cows[i].pos = ('Z', 0);
                    Cows[i].status = cowStatus.Dead;
                    Board.AllTiles[pos].Occupied = false;
                    break;
                }
            }
            return MoveError.Valid;
        }

    }
    
}

