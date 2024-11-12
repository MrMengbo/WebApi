using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemDTOesController : ControllerBase
    {
        // 定义函数，实现TodoItem到TodoItemDTO的转换。
        public static TodoItemDTO ItemToDTO(TodoItem todoItem) => new TodoItemDTO  //传入一个TodoItem，返回一个TodoItemDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete,
        };

        /*
         私有方法TodoItemExists()
         它接受一个 long 类型的参数 id，并返回一个布尔值（bool），表示在数据库中是否存在具有指定 id 的待办事项（TodoItem）。
         */
        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private readonly TodoContext _context;  //定义只读类型的数据库上下文

        public TodoItemDTOesController(TodoContext context)   // 定义Controller层的构造函数，进行数据库上下文的传参和调用
        {
            // 这样，控制器中的其他方法就可以通过_context字段与数据库进行交互了。
            _context = context;
        }

        [HttpGet("{id}")]   // 通过id查询单个，返回类型为ActionResult<TodoItemDTO>
        // async 和Task<TResult>是C# 异步编程的核心构建模块，配套使用，更高效、更安全
        // ActionResult<T>它是ActionResult的一个泛型版本，允许你指定返回内容的类型（在这个例子中是IEnumerable<TodoItemDTO>）。
        // ActionResult<T>提供了对返回内容的类型安全封装，并且当内容被序列化并写入HTTP响应体时，ASP.NET Core框架会自动处理它。
        public async Task<ActionResult<TodoItemDTO>> GetTodoItemById(long id) //返回的是TodoItemDTO
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return ItemToDTO(todoItem);  // 查询单个方法：将单个实体给返回
        }

        [HttpGet]   //查询所有,IEnumerable是一个接口，表示可以逐个枚举的T类型对象的集合，返回一个集合的遍历
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x=>ItemToDTO(x))
                .ToListAsync();
        }

        [HttpPut("{id}")]   //更新操作update，返回无内容响应
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)  //这边需要学习了解IActionResult的返回值，数据响应措施
        {
            if (id != todoDTO.Id)   // 参数一致性检查，检查URL中的id和请求实体中的todoDTO.ID是否一致
            {
                return BadRequest();
            }
            var todoItem = await _context.TodoItems.FindAsync(id); // 存在性检查，先检查待办事项是否存在，避免在数据库中执行不必要的操作
            if (todoItem == null)
            {
                return NotFound();  // 返回204状态码
            }
            todoItem.Id = todoDTO.Id;  //如果存在，才进行更新。下面这两行是更新的逻辑，将DTO对象映射给TodoItem实体类
            todoItem.Name = todoDTO.Name;
            try
            {
                await _context.SaveChangesAsync();  //异步将修改给保存到数据库中去。
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id)) //使用并发异常检查来处理数据的完整性和一致性。
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]   //增加操作Add
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            };

            _context.TodoItems.Add(todoItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItemById),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]   //删除，返回的类型也是IActionResult类型的，使用异步调用了。
        public async Task<IActionResult> DeleteTodoItem(long id) 
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) 
            { 
                return NotFound(); 
            }
            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
