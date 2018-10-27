using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using System.Web.Http;
using System.Web;
using NBench.Util;
using TaskManager.Service;
using TaskManger.DTOContext;
using TaskManager.Service.Controllers;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace TaskManager.Nbench
{
    public class CounterPerfSpecs: ApiController
    {
        private Counter _counter;
        TaskController taskController = new TaskController();
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
            
        }

        //[PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
        //    NumberOfIterations = 3, RunMode = RunMode.Throughput,
        //    RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        //[CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        //[MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        //[GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        //public void Benchmark()
        //{
        //    _counter.Increment();
        //}

        [PerfBenchmark(Description = "Get  task", NumberOfIterations = 5, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetTask()
        {
            
            var result = taskController.GetTaskList();
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get task", NumberOfIterations = 5, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetParentTask()
        {

            var result = taskController.ParentTask();
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get task by ID", NumberOfIterations = 5, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetTaskByID()
        {
            int taskID = 2;
            var result = taskController.GetTaskById(taskID);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Create Task", NumberOfIterations = 5, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void CreateTask()
        {
            TaskMangerContext taskObj = new TaskMangerContext();
            taskObj.Parent_ID = 1;
            taskObj.Task = "Task Performance";
            taskObj.Priority = 80;
            taskObj.StartDate = System.DateTime.Now;
            taskObj.EndDate = System.DateTime.Now;

            var result = taskController.CreateTask(taskObj);
            _counter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
