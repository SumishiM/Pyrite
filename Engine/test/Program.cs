// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Define masks for the flag section and the rest of the bits
const ulong FlagSectionMask = 0xFFFF000000000000; // Higher 16 bits mask
const ulong RestOfBitsMask = 0x0000FFFFFFFFFFFF; // Lower 48 bits mask
ulong n = 0;

n += UInt32.MaxValue;
n += ((ulong)UInt16.MaxValue << 32);
//n = n ^ (1ul << 63);

n |= 1ul << 62;
n |= ((ulong)0b1111 << 55);

Console.WriteLine("Integer: " + n);
Console.WriteLine("Binary String: " + Convert.ToString((long)n, 2).PadLeft(64, '0'));

Console.WriteLine("Binary String: " + Convert.ToString((long)((n & RestOfBitsMask) & ~(((ulong)0b1111 << 55) << 48)), 2).PadLeft(64, '0'));