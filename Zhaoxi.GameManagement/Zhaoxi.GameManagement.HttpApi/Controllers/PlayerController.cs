using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sample.GameManagement.Contracts;
using Zhaoxi.GameManagement.Entites;
using Zhaoxi.GameManagement.Entites.Dtos;
using Zhaoxi.GameManagement.Entites.RequestFeatures;

namespace Zhaoxi.GameManagement.HttpApi.Controllers
{
    [Route("api/player/[action]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;  //注册仓储服务
        private readonly ILogger<PlayerController> _logger;  //注册日志
        private readonly IMapper _mapper;

        public PlayerController(IRepositoryWrapper repository, ILogger<PlayerController> logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        /*
         IActionResult 是一个泛型接口，它允许控制器方法返回多种类型的操作结果。ASP.NET Core MVC 提供了许多实现了 IActionResult 接口的具体类，
        比如：
            ViewResult：返回一个视图给客户端，通常用于渲染 HTML 页面。
            ContentResult：返回纯文本内容给客户端。
            JsonResult：返回一个 JSON 对象给客户端，通常用于 AJAX 请求。
            RedirectResult：重定向客户端到另一个 URL。
            FileResult：返回一个文件给客户端，比如下载文件。
         
         */

        /*
        [FromQuery] PlayerParameter parameter：这是一个带有 [FromQuery] 属性的参数，表示它的值应该从请求的查询字符串中绑定。
        PlayerParameter 是一个自定义类，用于封装查询参数，比如分页信息。
         */

        [HttpGet]
        public IActionResult GetPlayers([FromQuery] PlayerParameter parameter)
        {
            try
            {
                var players = _repository.Player.GetPlayers(parameter);
                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(players.MetaData));

                var result = _mapper.Map<IEnumerable<PlayerDto>>(players);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPlayers() // 异步的返回值
        {
            try
            {
                var players = await _repository.Player.GetAllPlayers();  // 异步的方法
                var result = _mapper.Map<IEnumerable<PlayerDto>>(players); //将Player对象映射给playerDto对象
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }

        }


        [HttpGet("{id}", Name = "PlayerById")]
        public async Task<IActionResult> GetPlayerById(Guid id)  // 通过异步的方式，相关的关键字为：async和 await
        {
            try
            {
                var player = await _repository.Player.GetPlayerById(id); //先通过id找到这条数据
                if (player is null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<PlayerDto>(player); //然后映射给playerdto，来显示部分可读数据
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/character")]
        public async Task<IActionResult> GetPlayerWithCharacters(Guid id)
        {
            try
            {
                var player = await _repository.Player.GetPlayerWithCharacters(id);
                if (player is null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<PlayerWithCharactersDto>(player);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerForCreationDto player)
        {
            try
            {
                if (!ModelState.IsValid)  // 使用模型验证
                {
                    return BadRequest("无效的请求数据");
                }

                var playerEntity = _mapper.Map<Player>(player); //先从playerDTO映射成为player的实体

                _repository.Player.Create(playerEntity);
                await _repository.Save();

                var createdPlayer = _mapper.Map<PlayerDto>(playerEntity);
                return CreatedAtRoute("PlayerById", new { id = createdPlayer.Id }, createdPlayer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody] PlayerForUpdateDto player)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("无效的请求对象");
                }

                var playerEntity = await _repository.Player.GetPlayerById(id);
                if (playerEntity is null)
                {
                    return NotFound("待修改的玩家不存在");
                }

                _mapper.Map(player, playerEntity);

                _repository.Player.Update(playerEntity);
                await _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            try
            {
                var player = await _repository.Player.GetPlayerWithCharacters(id);
                if (player is null)
                {
                    return BadRequest("该玩家不存在");
                }

                if (player.Characters.Any())
                {
                    return BadRequest("该玩家有关联人物角色，不能删除！");
                }

                _repository.Player.Delete(player);
                await _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
