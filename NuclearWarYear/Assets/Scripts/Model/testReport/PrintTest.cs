using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model.testReport
{
    public class PrintTest
    {
        public void PrintCommandList(List<CommandLider> testList)
        {
            foreach (var test in testList) {
                Debug.Log("  test = "+ test.GetNameCommand()+" year "+ test.IncidentCommand.Year);
            
            }
        }
    }
}
