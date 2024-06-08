using System;
using Cube;
using NUnit.Framework;
using Tests.Edit;
using Tests.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests.RunTime
{
    public class CubeControllerTests
    {
        private CubeController _cubeController;
        
        [SetUp]
        public void SetUp()
        {
            var prefab = TestHelper.GetTestReference().UI;
            GameObject.Instantiate(prefab);
            
            var cubeManager = TestsTool.InitCubeManager();

            var controllers = GameObject.FindObjectsOfType<CubeController>();
            if (controllers.Length== 0)
            {
                throw new Exception("Can find CubeController in UITest");
            }
            _cubeController = controllers[0];
            _cubeController.Init(cubeManager);
            
        }

        [Test]
        public void InitCube_CreateCube()
        {
            int row = 5;
            int column = 3;
            _cubeController.InitCubes(row,column);

            Assert.AreEqual(row, _cubeController.Cubes.Count);

            foreach (var cube in _cubeController.Cubes)
            {
                Assert.AreEqual(column, cube.Count);
            }
        }
    }
}