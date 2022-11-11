using System.Runtime.InteropServices;
using System.Text;

namespace MetaTradeDemo;

/// <summary>
/// MetaTrade 接口
/// </summary>
public static class MetaTrade
{
	/* ---------------------------------------------------------------------------------
	 * 简单说明:
	 * 1.此文件为 .Net 与 MetaTrade 接口混合调用的接口文件
	 * 2.通常不需要修改此文件内容, 除非你非常了解 .Net 与 C++ 的调用规范
	 * 3.本接口在 .net6 下测试通过, .net6 为当前长期支持版本, 强烈建议您也使用这个版本
	 * ---------------------------------------------------------------------------------
	 */

	/// <summary>
	/// 初始化接口
	/// 调用此方法将会检验授权, 并返回授权的账户数量
	/// 建议在程序启动时, 调用此方法, 以便再进行后续操作
	/// </summary>
	/// <returns>已授权的账户数</returns>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern int Init();

	/// <summary>
	/// 反初始化接口
	/// 建议在程序退出出调用些方法
	/// </summary>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void Deinit();

	/// <summary>
	/// 登录账户
	/// </summary>
	/// <param name="ip">服务端 Ip 地址</param>
	/// <param name="port">端口号</param>
	/// <param name="version">版本号, 默认为空</param>
	/// <param name="yybId">营业部, 默认为零</param>
	/// <param name="account">账号, 同授权账号</param>
	/// <param name="tradeAccount">资金账号</param>
	/// <param name="jyPassword">交易密码</param>
	/// <param name="txPassword">通讯密码</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	/// <returns>客户端 Id</returns>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern int Logon(string ip, short port, string version, short yybId, string account, string tradeAccount, string jyPassword, string txPassword, StringBuilder errorInfo);

	/// <summary>
	/// 注销账户
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void Logoff(int clientId);

	#region + QueryData / SendOrder / CancelOrder / GetQuote / Repay

	/// <summary>
	/// 查询各种交易数据
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category">信息种类：0资金，1股份，2当日委托，3当日成交，4可撤单，5股东代码，6融资余额，7融券余额，8可融证券</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void QueryData(int clientId, int category, StringBuilder result, StringBuilder errorInfo);

	/// <summary>
	/// 委托下单
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category">委托种类：0买入，1卖出，2融资买入，3融券卖出，4买券还券，5卖券还款，6现券还券</param>
	/// <param name="entrustType">委托类型：0上海限价委托；深圳限价委托，1深圳对方最优价格，2深圳本方最优价格，3深圳即时成交剩余撤销，4上海五档即成剩撤；深圳五档即成剩撤，5深圳全额成交或撤销，6上海五档即成转限价</param>
	/// <param name="gddm">股东代码</param>
	/// <param name="zqdm">证券代码</param>
	/// <param name="price">委托价格</param>
	/// <param name="quantity">委托数量</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void SendOrder(int clientId, int category, int entrustType, string gddm, string zqdm, float price, int quantity, StringBuilder result, StringBuilder errorInfo);

	/// <summary>
	/// 委托撤单
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="exchangeId">交易所 Id：A1上海，A0深圳（部分券商是2）</param>
	/// <param name="entrustId">委托编号</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void CancelOrder(int clientId, string exchangeId, string entrustId, StringBuilder result, StringBuilder errorInfo);

	/// <summary>
	/// 查询五档行情
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="zqdm">证券代码</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void GetQuote(int clientId, string zqdm, StringBuilder result, StringBuilder errorInfo);

	/// <summary>
	/// 融资融券直接还款
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="amount">还款金额</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void Repay(int clientId, string amount, StringBuilder result, StringBuilder errorInfo);

	#endregion

	#region + QueryHistoryData / QueryDatas / SendOrders / CancelOrders / GetQuotes

	/// <summary>
	/// 查询各种历史交易数据
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category"></param>
	/// <param name="startDate">开始日期，格式为 yyyyMMdd，如 20150601</param>
	/// <param name="endDate">结束日期，格式为 yyyyMMdd，如 20150801</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void QueryHistoryData(int clientId, int category, string startDate, string endDate, StringBuilder result, StringBuilder errorInfo);

	/// <summary>
	/// 批量查询各类交易数据
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category">信息种类：0资金，1股份，2当日委托，3当日成交，4可撤单，5股东代码，6融资余额，7融券余额，8可融证券</param>
	/// <param name="count">查询数量, 即参数数组的长度</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void QueryDatas(int clientId, int[] category, int count, IntPtr[] result, IntPtr[] errorInfo);

	/// <summary>
	/// 委托下单
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category">委托种类：0买入，1卖出，2融资买入，3融券卖出，4买券还券，5卖券还款，6现券还券</param>
	/// <param name="entrustType">委托类型：0上海限价委托；深圳限价委托，1深圳对方最优价格，2深圳本方最优价格，3深圳即时成交剩余撤销，4上海五档即成剩撤；深圳五档即成剩撤，5深圳全额成交或撤销，6上海五档即成转限价</param>
	/// <param name="gddm">股东代码</param>
	/// <param name="zqdm">证券代码</param>
	/// <param name="price">委托价格</param>
	/// <param name="quantity">委托数量</param>
	/// <param name="count">下单数量, 即参数数组的长度</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void SendOrders(int clientId, int[] category, int[] entrustType, string[] gddm, string[] zqdm, float[] price, int[] quantity, int count, IntPtr[] result, IntPtr[] errorInfo);

	/// <summary>
	/// 委托撤单
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="exchangeId">交易所 Id：A1上海，A0深圳（部分券商是2）</param>
	/// <param name="entrustId">委托编号</param>
	/// <param name="count">撤单数量, 即参数数组的长度</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void CancelOrders(int clientId, string[] exchangeId, string[] entrustId, int count, IntPtr[] result, IntPtr[] errorInfo);

	/// <summary>
	/// 查询五档行情
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="zqdm">证券代码</param>
	/// <param name="count">查询数量, 即参数数组的长度</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void GetQuotes(int clientId, string[] zqdm, int count, IntPtr[] result, IntPtr[] errorInfo);

	#endregion

	#region + QueryMultiAccountsDatas / SendMultiAccountsOrders / CancelMultiAccountsOrders / GetMultiAccountsQuotes

	/* ---------------------------------------------------------------------------------
	 * 此部分为跨账户操作，部分券商可能会不支持，重要逻辑建议使用上面有接口，以保证数据准确性
	 * ---------------------------------------------------------------------------------
	 */

	/// <summary>
	/// 跨账户查询各种交易数据
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category">信息种类：0资金，1股份，2当日委托，3当日成交，4可撤单，5股东代码，6融资余额，7融券余额，8可融证券</param>
	/// <param name="count">操作数量</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void QueryMultiAccountsDatas(int[] clientId, int[] category, int count, IntPtr[] result, IntPtr[] errorInfo);

	/// <summary>
	/// 跨账户委托下单
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="category">委托种类：0买入，1卖出，2融资买入，3融券卖出，4买券还券，5卖券还款，6现券还券</param>
	/// <param name="entrustType">委托类型：0上海限价委托；深圳限价委托，1深圳对方最优价格，2深圳本方最优价格，3深圳即时成交剩余撤销，4上海五档即成剩撤；深圳五档即成剩撤，5深圳全额成交或撤销，6上海五档即成转限价</param>
	/// <param name="gddm">股东代码</param>
	/// <param name="zqdm">证券代码</param>
	/// <param name="price">委托价格</param>
	/// <param name="quantity">委托数量</param>
	/// <param name="count">操作数量</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void SendMultiAccountsOrders(int[] clientId, int[] category, int[] entrustType, string[] gddm, string[] zqdm, float[] price, int[] quantity, int count, IntPtr[] result, IntPtr[] errorInfo);

	/// <summary>
	/// 跨账户委托撤单
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="exchangeId">交易所 Id：A1上海，A0深圳（部分券商是2）</param>
	/// <param name="entrustId">委托编号</param>
	/// <param name="count">操作数量</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void CancelMultiAccountsOrders(int[] clientId, string[] exchangeId, string[] entrustId, int count, IntPtr[] result, IntPtr[] errorInfo);

	/// <summary>
	/// 跨账户查询五档行情
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <param name="zqdm">证券代码</param>
	/// <param name="count">操作数量</param>
	/// <param name="result">返回信息，一般要分配1024*1024字节的空间，出错时为空字符串。</param>
	/// <param name="errorInfo">错误信息，一般要分配256字节的空间，出错时为非空字符串</param>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern void GetMultiAccountsQuotes(int[] clientId, string[] zqdm, int count, IntPtr[] result, IntPtr[] errorInfo);

	#endregion

	#region + GetExpireDate

	/// <summary>
	/// 查询授权到期时间
	/// </summary>
	/// <param name="clientId">客户端 Id</param>
	/// <returns></returns>
	[DllImport("MetaTrade.dll", CharSet = CharSet.Ansi)]
	public static extern int GetExpireDate(int clientId);

	#endregion
}