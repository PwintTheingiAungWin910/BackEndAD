﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEndAD.Models;
using BackEndAD.ServiceInterface;
using BackEndAD.DataContext;
using BackEndAD.ServiceInterface;
using System.Text.RegularExpressions;

//REMINDER: All existing comments generated by BiancaZYCao
//This is an simple example about how to code Web API controller return data result for ReactJS
//
namespace BackEndAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        //GET DATA result from SERVICE layer 
        //e.g. here IEmployeeService is an interface do all the stuff related to ENTITY- emp
        //Here we should call fewer service to make code reusable and clean 
        //private IEmployeeService _empService; not used so far 
        private IDepartmentService _deptService;

        //CONSTRUCTOR: make sure u build ur service interface in.
        public DeptController(IDepartmentService deptService)
        {
            _deptService = deptService;
        }

        // CONTROLLER METHODS handling each HTTP get/put/post/request 
        // GET: api/Dept
        [HttpGet]
        public async Task<ActionResult<IList<Department>>> GetAllDepartments()
        {
            var result = await _deptService.findAllDepartmentsAsync();
            // if find data then return result else will return a String says Department not found
            if (result != null)
                //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
                return Ok(result);
            else
                //this help to return a NOTfOUND result, u can customerize the string.
                //There are 3 Department alr seeded in DB, so this line should nvr appears. 
                //I put here Just for u to understand the style. :) -Bianca  
                return NotFound("Departments not found");
        }

        //This is not finished! -Bianca
        [HttpGet("eager")]
        public ActionResult<List<CollectionInfo>> GetAllDepartmentsEager()
        {
            var resultL = _deptService.findAllDepartmentsAsyncEager();
            var result = new List<CollectionInfo>(){ };
            
            //var result = new List<Int64>() { };
            foreach (Department dept in resultL)
            {
                Console.WriteLine(dept.Collection.lat);
                result.Add(dept.Collection);
             }
            if (result != null)
            {
                var result2 = result.First<CollectionInfo>();
                return Ok(result2);//.First<Department>().Collection.Id);
            }
            else
                return NotFound("Eager No way!");
        }

        [HttpGet("req")]
        public async Task<ActionResult<IList<Requisition>>> GetAllRequisitions()
        {
            var result = await _deptService.findAllRequsitionsAsync();
            //var result2 = result.First<Requisition>().Employee;
            // if find data then return result else will return a String says Department not found
            if (result != null)
                //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
                return Ok(result);
            else
                //this help to return a NOTfOUND result, u can customerize the string.
                //There are 3 Department alr seeded in DB, so this line should nvr appears. 
                //I put here Just for u to understand the style. :) -Bianca  
                return NotFound("Requisition not found");
        }

        [HttpGet("pendingReq")]
        public async Task<ActionResult<IList<Requisition>>> GetPendingRequisitions()
        {
	        var result = await _deptService.findAllRequsitionsAsync();
	        var result_filtered = result.Where(x => x.status == "Applied");

            if (result_filtered != null)
		        //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
		        return Ok(result_filtered);
	        else
		        return NotFound("No pending requisition found");
        }

        [HttpGet("emp")]
        public async Task<ActionResult<IList<Employee>>> GetAllEmployees()
        {
            var result = await _deptService.findAllEmployeesAsync();
           
            // if find data then return result else will return a String says Department not found
            if (result != null)
                //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
                return Ok(result);
            else
                //this help to return a NOTfOUND result, u can customerize the string.
                //There are 3 Department alr seeded in DB, so this line should nvr appears. 
                //I put here Just for u to understand the style. :) -Bianca  
                return NotFound("Requisition not found");
        }

        [HttpGet("deptEmp/{id}")]
        public async Task<ActionResult<IList<Employee>>> GetAllEmployeesByDept(int id)
        {
	        var result = await _deptService.findAllEmployeesAsync();
	        var result_filtered = result.Where(x => x.departmentId == id);

            if (result_filtered != null)
	            return Ok(result_filtered);
	        else
	            return NotFound("Employees not found");
        }

        //return dept info by id
        // GET: api/Dept/1
        // data passing via URL 
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentByIdAsync(int id)
        {
            var result = await _deptService.findDepartmentByIdAsync(id);
            // if find data then return result else will return a String says Department not found
            if (result != null)
                return Ok(result);
            else
                return NotFound("Department not found");
        }

        //this not work Sry Idk details, it is weird. -Bianca
        // GET: api/dept/search?name=ComputerScience
        /*
        [HttpGet("search")]
        public ActionResult<Department> Search(string name)
        {
            Regex r = new Regex(@"(?!^)(?=[A-Z])");
            String nameWithSpace = r.Replace(name, "");
            var dept = _deptService.findDepartmentByName(nameWithSpace);
            if (dept == null || name == null)
            {
                return null;
            }
            return dept;
        }*/



        /* We should use async methods here to improve efficiency
         * However, Here is a sample code for sync method -  getDeptById for u to get familiar with
         * public ActionResult<Department> GetDepartmentById(int id)
        {
            return  _deptService.findDepartmentById(id);
            // u also need findDeptById in your service layer and repo layer 
            (DO NOT FORGET INTERFACE and AddScoped<...> for BOTH repo and service)
        }*/

        [HttpGet("allCollectionpt")]
        public async Task<ActionResult<IList<CollectionInfo>>> GetAllCollectionPointforDept()
        {
	        IList<CollectionInfo> result = await _deptService.findAllCollectionPointAsync();

	        // if find data then return result else will return a String says Department not found
	        if (result != null)
		        //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
		        return Ok(result);
	        else
		        //this help to return a NOTfOUND result, u can customerize the string.
		        //There are 3 Department alr seeded in DB, so this line should nvr appears. 
		        //I put here Just for u to understand the style. :) -Bianca  
		        return NotFound("No colleciton point found");
        }

        [HttpGet("retrieval")]
        public async Task<ActionResult<IList<Requisition>>> GetAllPendingRequisitions()
        {
            var result = await _deptService.findAllRequsitionsAsync();

            var result_filtered = result.Where(x => x.status != "Delivered");
            foreach (var x in result_filtered)
            {
                Console.WriteLine(x.Id);
            }
            var result_filtered2 = result_filtered.Where(x => x.status != "Declined");
            foreach (var x in result_filtered2)
            {
                Console.WriteLine(x.Id);
            }

            if (result_filtered2 != null)
                //convert to json file
                return Ok(result_filtered2);
            else
                //in case there is nothing to process
                return NotFound("No pending requistions");
        }


    }
}