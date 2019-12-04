using System;
using System.Collections.Generic;
using System.Linq;

namespace Christmas.Day3
{
    public class Wire
    {
        private string Path { get; }
        public readonly Tuple<int,int> Start;
        public readonly Tuple<int,int> End;

        public Wire(string path)
        {
            Path = path;
            Start = new Tuple<int, int>(0,0);
            End = SetEndCoordinates(path);
        }

        private static Tuple<int, int> SetEndCoordinates(string path)
        {
            var x = SumOfDirections(path,"R") - SumOfDirections(path,"L");
            var y = SumOfDirections(path, "U") - SumOfDirections(path,"D");
            return new Tuple<int, int>(x,y);
        }

        private static int SumOfDirections(string path, string direction)
        {
            return path.Split(',')
                .Where(s => s.StartsWith(direction))
                .Select(s => (int.TryParse(s.Replace(direction,""), out var number)) ? number : 0)
                .Sum();
        }

        public bool DoesIntersect(int pointX, int pointY)
        {
            var intersections = GetIntersections();
            return intersections.ContainsKey($"{pointX},{pointY}");
        }

        public Dictionary<string, int> GetIntersections()
        {
            var intersections = new Dictionary<string, int>();
            var points = Path.Split(',');
            var x = Start.Item1;
            var y = Start.Item2;
            var position = 0;
            foreach (var move in points)
            {
                var moveX = SumOfDirections(move, "R");
                var moveNegativeX = SumOfDirections(move, "L");
                var moveY = SumOfDirections(move, "U");
                var moveNegativeY = SumOfDirections(move, "D");
                
                for (var i = 1; i <= moveX; i++)
                {
                    var key = $"{x + i},{y}";
                    position++;
                    if (!intersections.ContainsKey(key)) intersections.Add(key, position);
                }

                for (var i = 1; i <= moveNegativeX; i++)
                {
                    var key = $"{x - i},{y}";
                    position++;
                    if (!intersections.ContainsKey(key)) intersections.Add(key, position);
                }

                for (var i = 1; i <= moveY; i++)
                {
                    var key = $"{x},{y + i}";
                    position++;
                    if (!intersections.ContainsKey(key)) intersections.Add(key, position);
                }

                for (var i = 1; i <= moveNegativeY; i++)
                {
                    var key = $"{x},{y - i}";
                    position++;
                    if (!intersections.ContainsKey(key)) intersections.Add(key, position);
                }

                x += moveX - moveNegativeX;
                y += moveY - moveNegativeY;
            }

            if (intersections.ContainsKey("0,0")) intersections.Remove("0,0");
            return intersections;
        }

        public bool DoesIntersect(string testPath)
        {
            var x = Start.Item1;
            var y = Start.Item2;
            
            var points = testPath.Split(',');
            foreach (var point in points)
            {
                var moveX = SumOfDirections(point, "R");
                var moveNegativeX = SumOfDirections(point, "L");
                var moveY = SumOfDirections(point, "U");
                var moveNegativeY = SumOfDirections(point, "D");
                for (var i = 1; i <= moveX; i++)
                {
                    if (DoesIntersect(x+i, y)) return true;
                }
                for (var i = 1; i <= moveNegativeX; i++)
                {
                    if (DoesIntersect(x-i, y)) return true;
                }
                for (var i = 1; i <= moveY; i++)
                {
                    if (DoesIntersect(x, y+i)) return true;
                }
                for (var i = 1;i <= moveNegativeY; i++)
                {
                    if (DoesIntersect(x, y-1)) return true;
                }

                x += moveX - moveNegativeX;
                y += moveY - moveNegativeY;
            }
            return false;
        }

        public static IEnumerable<string> CommonIntersections(Dictionary<string,int> list1, Dictionary<string,int> list2)
        {
            var l1 = list1.Select(kvp => kvp.Key).ToList();
            var l2 = list2.Select(kvp => kvp.Key).ToList();
            return l1.Intersect(l2).ToList();
        }

        public int ClosestIntersect(string path)
        {
            var testWire = new Wire(path);
            var seta = GetIntersections();
            var setb = testWire.GetIntersections();
            var commonIntersections = CommonIntersections(seta,setb);
            var shortestDistance = 0;
            
            foreach (var intersection in commonIntersections)
            {
                var distance = ManhattenDistance(intersection);
                if (shortestDistance == 0 || shortestDistance > distance) shortestDistance = distance;
            }

            return shortestDistance;
        }

        public static int ManhattenDistance(string intersection)
        {
            return intersection
                .Split(',')
                .Select(s => int.TryParse(s,out var number) ? number : 0)
                .Select(i => i<0 ? i*-1 : i)
                .Sum();
        }

        public int GetShortestIntersect(string path)
        { 
            var testWire = new Wire(path);
            var seta = GetIntersections();
            var setb = testWire.GetIntersections();
            var commonIntersections = CommonIntersections(seta,setb);
            var distance = 0;
            foreach(var intersection in commonIntersections)
            {
                var i1 = seta[intersection] + setb[intersection];
                if (distance == 0 || distance > i1)
                {
                    distance = i1;
                }
            }

            return distance;
        }
    }
}