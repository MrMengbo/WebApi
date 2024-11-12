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
    // 定义TodoItemsController类，它继承自ControllerBase类，后者提供了处理HTTP请求的基本功能。
    public class TodoItemsController : ControllerBase
    {
        // 声明一个私有只读字段_context，其类型为TodoContext。TodoContext是一个数据库上下文类，用于与数据库交互。
        private readonly TodoContext _context;

        /*
         定义TodoItemsController的构造函数，它接收一个TodoContext类型的参数context。
         在构造函数体内，将传入的context参数赋值给_context字段。
         这样，控制器中的其他方法就可以通过_context字段与数据库进行交互了。
         */
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()  //查询所有
        {
            //TodoContext是一个继承自DbContext的类，这是EF Core的核心类，用于表示数据库会话。
            //在TodoContext中，你通常会定义DbSet<T>属性，
            //即public DbSet<TodoItem> TodoItems { get; set; }，这样EF Core就知道如何与数据库中的TodoItems表进行交互

            /*
             由于ToListAsync()和FindAsync()是EF Core扩展方法，它们作用于DbSet<T>实例上，
            这就是为什么你能够在_context.TodoItems上调用这些方法，而不需要在TodoContext中显式定义它们。
             */
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]

        // 返回值和方法的返回类型为ActionResult<T>类型，ASP.NET Core 会自动将对象序列化为 JSON，并将 JSON 写入响应消息的正文
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)  //根据id查询
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // 修改
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)  //修改，根据id和传入的数据进行update
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        /*
         定义一个HTTP POST方法，用于处理创建新的待办事项的请求。
         该方法接收一个TodoItem类型的参数todoItem，表示要创建的待办事项。
         方法被标记为async，表示它是异步的，可以使用await关键字等待异步操作的完成。
         ActionResult<TodoItem>是返回类型，表示该方法可以返回一个TodoItem对象或者一个表示操作结果的其他类型的对象。
         */
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem) // 增加add
        {
            // 使用_context.TodoItems.Add(todoItem)方法将传入的todoItem对象添加到数据库上下文的TodoItems集合中。
            // 这实际上是在内存中创建了待办事项的一个实例，但还没有将其保存到数据库中。
            _context.TodoItems.Add(todoItem);

            // 调用_context.SaveChangesAsync()方法异步地将内存中的更改保存到数据库中。
            // await关键字用于等待SaveChangesAsync()方法的完成，而不会阻塞当前线程。
            await _context.SaveChangesAsync();

            // 下面两行代码是返回语句，用于向客户端发送HTTP 201 Created响应，表示新的资源（待办事项）已被创建。
            // CreatedAtAction方法用于生成一个包含新创建资源URI的响应头（Location）。
            // 第一个参数是创建资源后用于获取该资源的动作方法名称（在这里是GetTodoItem）。
            // 第二个参数是一个路由值字典，包含生成URI所需的路由参数（在这里是id，其值为新创建的待办事项的Id）。
            // 第三个参数是新创建的待办事项对象，将作为响应正文返回给客户端。
            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new {id=todoItem.Id},todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)   // 删除，通过id
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);  // 先执行删除操作。
            await _context.SaveChangesAsync();  //然后将内存中的更改保存到数据库中去。

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
