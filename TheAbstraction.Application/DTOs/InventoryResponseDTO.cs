namespace TheAbstraction.Application.DTOs;

public class InventoryResponseDTO
{
    public string PlayerId { get; set; }

    public List<ItemInventoryResponseDTO> Items { get; set; }
}