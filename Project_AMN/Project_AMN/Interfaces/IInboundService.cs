namespace Project_AMN.Interfaces;

/// <summary>
/// Defines service methods for managing article inbounds.
/// </summary>
public interface IInboundService
{
    /// <summary>
    /// Registers an inbound shipment for a specific article by SKU and quantity.
    /// Returns true if successful, false if the article is not found.
    /// </summary>
    Task<bool> RegisterInboundAsync(string sku, int quantity);
    
} 