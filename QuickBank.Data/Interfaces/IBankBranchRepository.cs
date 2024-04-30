namespace QuickBank.Data.Interfaces
{
    public interface IBankBranchRepository
    {
        Task<bool> DoesBankBranchExistsAsync(string bankCode, string reservedCharacter, string branchCode);
    }
}
