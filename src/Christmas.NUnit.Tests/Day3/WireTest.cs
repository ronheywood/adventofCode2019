using System;
using System.Collections.Generic;
using System.Linq;
using Christmas.Day3;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day3
{
    public class WireTest
    {
        [Test]
        public void Should_have_starting_point_from_center_point()
        {
            var wire = new Wire("");
            var expected = new Tuple<int, int>(0, 0);
            Assert.That(wire.Start, Is.EqualTo(expected));
        }
        [TestCase("",0)]
        [TestCase("R1",1)]
        [TestCase("R2",2)]
        [TestCase("R10",10)]
        public void Should_calculate_end_point_right(string path, int expectedX)
        {
            var wire = new Wire(path);
            var expected = new Tuple<int, int>(expectedX,0);
            Assert.That(wire.End, Is.EqualTo(expected));
        }
        
        [TestCase("",0)]
        [TestCase("L1",-1)]
        [TestCase("L2",-2)]
        [TestCase("L10",-10)]
        public void Should_calculate_end_point_left(string path, int expectedX)
        {
            var wire = new Wire(path);
            var expected = new Tuple<int, int>(expectedX,0);
            Assert.That(wire.End, Is.EqualTo(expected));
        }
        
        [TestCase("",0)]
        [TestCase("U1",1)]
        [TestCase("U2",2)]
        [TestCase("U10",10)]
        public void Should_calculate_end_point_up(string path, int expectedY)
        {
            var wire = new Wire(path);
            var expected = new Tuple<int, int>(0,expectedY);
            Assert.That(wire.End, Is.EqualTo(expected));
        }
        
        [TestCase("",0)]
        [TestCase("D1",-1)]
        [TestCase("D2",-2)]
        [TestCase("D10",-10)]
        public void Should_calculate_end_point_down(string path, int expectedY)
        {
            var wire = new Wire(path);
            var expected = new Tuple<int, int>(0,expectedY);
            Assert.That(wire.End, Is.EqualTo(expected));
        }
        
        [TestCase("U5",5,"0,1")]
        [TestCase("U7,R6,D4,L4",21,"3,3")]
        [TestCase("U7,L6,D4",17,"-6,7")]
        public void Should_calculate_points_used_in_a_path(string path,int count,string contains)
        {
            var wire = new Wire(path);
            Assert.That(wire.GetIntersections().Count, Is.EqualTo(count));
            var actual = wire.GetIntersections().Select(kvp => kvp.Key).ToList();

            Assert.That(actual, Does.Not.Contain("0,0"));
            Assert.That(actual, Does.Contain(contains),$"{string.Join("|",actual)}");
        }

        [Test]
        public void Expect_intersections_to_filter_to_commoon()
        {
            var l1 = new Dictionary<string,int>{
                {"0,1",1},
                {"0,2",2},
                {"3,3",3}, 
                {"5,6",4}};

            var l2 = new Dictionary<string,int>
            {
                {"1,0",1},
                {"2,2",2},
                {"3,3",3},
                {"5,6",4}};;

            var common = Wire.CommonIntersections(l1,l2);
            Assert.That(common.Count, Is.EqualTo(2));
            Assert.That(common, Does.Contain("3,3"));
            Assert.That(common, Does.Contain("5,6"));
        }
        
        [TestCase("",0,0)]
        [TestCase("D1,U1",0,0)]
        [TestCase("D2,U1",0,-1)]
        [TestCase("D1,L1",-1,-1)]
        [TestCase("U1,R1",1,1)]
        [TestCase("R8,U5,L5,D3,",3,2)]
        public void Should_calculate_endpoint_from_chain_of_paths(string path, int expectedX, int expectedY)
        {
            var wire = new Wire(path);
            var expected = new Tuple<int, int>(expectedX,expectedY);
            Assert.That(wire.End, Is.EqualTo(expected));
        }
        [TestCase("R8,U5,L5,D3","1,0",true)]
        [TestCase("R8,U5,L5,D3","2,0",true)]
        [TestCase("D5,R2,D2,L5","0,-5",true)]
        [TestCase("L10,U2,R8,D4","-8,2",true)]
        public void Should_report_if_intersects_a_point(string path, string test, bool expectIntersect)
        {
            var wire = new Wire(path);
            var points = test.Split(',').Select(s => int.TryParse(s, out var number) ? number : 0).ToArray();
            Assert.That(wire.DoesIntersect(points[0], points[1]), Is.EqualTo(expectIntersect));
        }
        [TestCase("R3","R1",true)]
        [TestCase("L1","R1",false)]
        [TestCase("U1","D1",false)]
        [TestCase("U1","U1",true)]
        [TestCase("D1","D1",true)]
        [TestCase("L1","L1",true)]
        [TestCase("R1","R1",true)]
        [TestCase("L2,U2,R2,U2","R2,U2,L2",true)]
        [TestCase("L2,U2,R2,U2","R2,U2,R2",false)]
        [TestCase("R8,U5,L5,D3","U7,R6,D4,L4",true)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72","U62,R66,U55,R34,D71,R55,D58,R83",true)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51","U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",true)]
        public void Should_report_if_intersects_a_path(string wirePath, string testPath, bool expectIntersect)
        {
            var wire = new Wire(wirePath);
            Assert.That(wire.DoesIntersect(testPath), Is.EqualTo(expectIntersect));
        }
        [TestCase("3,3",6)]
        [TestCase("-2,0",2)]
        [TestCase("2,-2",4)]
        [TestCase("-10,-2",12)]
        public void ManhattenDistance_should_be_distance_x_plus_distance_y(string xy,int expected)
        {
            Assert.That(Wire.ManhattenDistance(xy), Is.EqualTo(expected));
        }
        
        [TestCase("R8,U5,L5,D3","U7,R6,D4,L4",6)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72","U62,R66,U55,R34,D71,R55,D58,R83",159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51","U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",135)]
        public void Should_return_the_closest_intersection_to_the_center(string path1,string path2,int expectedDistance)
        {
            var wire = new Wire(path1);
            Assert.That(wire.DoesIntersect(path2), Is.EqualTo(true), "Wires do not cross");
            Assert.That(wire.ClosestIntersect(path2), Is.EqualTo(expectedDistance));
        }

        [TestCase("R8,U5,L5,D3","U7,R6,D4,L4","3,3",20,20)]
        [TestCase("R8,U5,L5,D3","U7,R6,D4,L4","6,5",15,15)]
        public void Intersections_have_steps(string path1, string path2, string intersection,int expectedSteps1,int expectedSteps2)
        {
            var wire1 = new Wire(path1).GetIntersections();
            var wire2 = new Wire(path2).GetIntersections();

            Assert.That(wire1.ContainsKey(intersection), Is.True);
            Assert.That(wire2.ContainsKey(intersection), Is.True);
            Assert.That(wire1[intersection], Is.EqualTo(expectedSteps1));
        }
        [TestCase("R8,U5,L5,D3","U7,R6,D4,L4",30)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72","U62,R66,U55,R34,D71,R55,D58,R83",610)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51","U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",410)]
        public void Should_get_shortest_intersect(string path1, string path2, int expected)
        {
            var wire1 = new Wire(path1);
            Assert.That(wire1.GetShortestIntersect(path2), Is.EqualTo(expected));
        }
    }
}
