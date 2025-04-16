using assignment9.Models;
using Microsoft.AspNetCore.Mvc;
using assignment9.Service;
using Microsoft.Extensions.Logging;

namespace assignment9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(OrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders()
        {
            try
            {
                _logger.LogInformation("Fetching all orders...");
                var orders = _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all orders.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching order with ID {id}...");
                var order = _orderService.GetOrderById(id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound($"Order with ID {id} not found.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching order with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public ActionResult AddOrder([FromBody] Order order)
        {
            try
            {
                _logger.LogInformation("Adding a new order...");
                _orderService.AddOrder(order);
                return Ok("Order added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order.");
                return BadRequest($"Error adding order: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                if (id != order.Id)
                {
                    _logger.LogWarning("Order ID mismatch.");
                    return BadRequest("Order ID mismatch.");
                }
                _logger.LogInformation($"Updating order with ID {id}...");
                _orderService.UpdateOrder(order);
                return Ok("Order updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating order with ID {id}.");
                return BadRequest($"Error updating order: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting order with ID {id}...");
                _orderService.DeleteOrder(id);
                return Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting order with ID {id}.");
                return BadRequest($"Error deleting order: {ex.Message}");
            }
        }
    }
}