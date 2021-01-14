using System;
using System.Configuration;
using System.Data.SqlClient;

namespace VIP.SystemService.DB
{

	/// <summary>
	/// Conn 的摘要说明。
	/// 提供数据库连接字段。
	/// </summary>
	public class Conn
	{
		private readonly static string m_connString;

		static Conn()
		{
			// initialize the database connection string 
			m_connString = "data source=10.16.4.22;user id=sa;password=ATF.db.test?;persist security info=True;initial catalog = gstest;Connect Timeout=600;Pooling=true; MAX Pool Size=512;Min Pool Size=50;Connection Lifetime=30";
			 
		}

		public static string getConnStr()
		{
			return m_connString;	
		}

		/// <summary>
		/// 通过读取配置文件来获得数据库连接字符串
		/// </summary>
		/// <param name="name">标识数据库连接的简单字符串</param>
		/// <returns>数据库连接字符串</returns>
		public static SqlConnection getConn(string name)
		{
			SqlConnection conn = new SqlConnection(m_connString); 
			conn.Open();
			return conn;
		}

		public static SqlConnection getConn()
		{
			SqlConnection conn = new SqlConnection(m_connString); 
			conn.Open();
			return conn;
		}

		/// <summary>
		/// 关闭链接
		/// </summary>
		/// <param name="conn"></param>
		public static void closeConn(SqlConnection conn)
		{
			try
			{
				conn.Close();
			}
			catch
			{
				// 处理链接异常
			}
		}
	}
}
