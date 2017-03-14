﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;

namespace HAL_Solver
{
    class MapLoad
    {
        public static void loadMap(string filename, out Map map)
        {
            int colcount = 0, rowcount = 0;
            getfilesize(filename, out colcount, out rowcount);
            Collection<Box> newboxes = new Collection<Box>();
            Collection<Actor> newactors = new Collection<Actor>();
            GoalList newgoals = new GoalList();
            Dictionary<char, Color> colorDict = new Dictionary<char, Color>();
            bool[,] newwallmap = new bool[colcount,rowcount];
            foreach (string line in File.ReadLines(@filename))
            {
                Byte j = 0; // row count
                if (line.Contains("+"))
                {
                    

                    Byte i = 0; // col count
                    //map construction
                    foreach (char c in line)
                    {
                        if (c == '+') { newwallmap[i, j] = true; }
                        else if (Char.IsLower(c)) { newgoals.Add(i, j, c); } // i,j is goal
                        else if (Char.IsDigit(c)) {// i,j is actor
                            if (colorDict.ContainsKey(c)) { newactors.Add(new Actor(i,j,colorDict[c], c)); }
                        } 
                        else if (Char.IsUpper(c)) {
                            if (colorDict.ContainsKey(c)) { newboxes.Add(new Box(i,j,colorDict[c], c)); }
                        } // i,j is box
                        i++;
                    }
                    j++;
                }
                else
                {
                    string[] splitline = line.Split(':');
                    string names = splitline[1].Remove(0, 1);
                    string[] splitnames = names.Split(',');
                    foreach (string name in splitnames)
                    {
                        colorDict[name[0]] = Color.FromName(splitline[0]);
                    }
                    Color slateBlue = Color.FromName("SlateBlue");
                    //do color devision
                }
            }
            map = new Map(newwallmap, newactors, newboxes, newgoals);

        }
        public static void getfilesize(string filename,out int colcount,out int rowcount)
        {
            colcount = 0;
            rowcount = 0;
            foreach (string line in File.ReadLines(@filename))
            {
                
                if (line.Contains("+")) {
                    rowcount++;
                    if (colcount< line.Length) { colcount = line.Length; }
                }
            }
        }
    }
}