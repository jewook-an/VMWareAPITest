using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace NCCRequireService.Util.PowerShell
{
	public class PowerCli : IDisposable
	{
		Runspace runSpace = null;

		public DataTable Run_Table(string Command)
		{
			string[] PrintCommandLines = Command.Split(new string[] { "\n" }, StringSplitOptions.None);

			//NCCFramework.Util.Logger.Debug(ref logger, $@"실행전 : Command = {string.Join("\r\n", PrintCommandLines)} / Now : {DateTime.Now}");

			DataTable _ret = new DataTable();

			try
			{
				using (runSpace = RunspaceFactory.CreateRunspace())
				{
					runSpace.Open();

					using (System.Management.Automation.PowerShell pwsh = System.Management.Automation.PowerShell.Create())
					{
						pwsh.Runspace = runSpace;
						pwsh.AddScript(Command);
						IAsyncResult gpcAsyncResult = pwsh.BeginInvoke();

						using (PSDataCollection<PSObject> ps_result = pwsh.EndInvoke(gpcAsyncResult))
						{
							foreach (PSObject psObject in ps_result)
							{
								DataRow row = _ret.NewRow();

								foreach (PSPropertyInfo prop in psObject.Properties)
								{
									if (prop != null)
									{
										if (prop.TypeNameOfValue.ToUpper().Contains("STRING[]"))
										{
											if (!_ret.Columns.Contains(prop.Name))
											{
												_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
											}
											string temp_prop_value = string.Empty;

											for (int temp_i = 0; prop.Value != null && temp_i < ((string[])prop.Value).Length; temp_i++)
											{
												temp_prop_value += ((string[])prop.Value)[temp_i] + ", ";
											}

											if (temp_prop_value.EndsWith(", "))
											{
												temp_prop_value = temp_prop_value.Substring(0, temp_prop_value.Length - 2);
											}

											row[prop.Name] = temp_prop_value;
										}
										else if (prop.TypeNameOfValue.ToUpper().Contains("DICTIONARY"))
										{
											if (!_ret.Columns.Contains(prop.Name))
											{
												_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
											}
											string temp_prop_value = string.Empty;

											row[prop.Name] = temp_prop_value;
										}
										else if (prop.TypeNameOfValue.ToUpper().Contains("NULLABLE"))
										{
											if (!_ret.Columns.Contains(prop.Name))
											{
												_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
											}

											try
											{
												row[prop.Name] = (prop.Value == null) ? "" : prop.Value;
											}
											catch
											{
												row[prop.Name] = "";
											}
										}
										else if (prop.TypeNameOfValue.ToUpper().Contains("INT"))
										{
											if (!_ret.Columns.Contains(prop.Name))
											{
												_ret.Columns.Add(new DataColumn(prop.Name, typeof(long)));
											}

											row[prop.Name] = prop.Value;
										}
										else
										{
											if (!_ret.Columns.Contains(prop.Name))
											{
												_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
											}

											try
											{
												row[prop.Name] = (prop.Value == null) ? "" : prop.Value;
											}
											catch
											{
												row[prop.Name] = "";
											}
										}
									}
								}

								_ret.Rows.Add(row);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				//Memory Leak
				try
				{
					runSpace = null;
					GC.Collect();
				}
				catch { }
			}

			return _ret;
		}

		public DataTable Run_Table(List<string> Commands)
		{
			DataTable _ret = new DataTable();

			try
			{
				using (runSpace = RunspaceFactory.CreateRunspace())
				{
					runSpace.Open();

					using (System.Management.Automation.PowerShell pwsh = System.Management.Automation.PowerShell.Create())
					{
						pwsh.Runspace = runSpace;

						bool IsAddScript = false;
						foreach (string Command in Commands ?? Enumerable.Empty<string>())
						{
							if (!string.IsNullOrEmpty(Command))
							{
								pwsh.AddScript(Command);
								IsAddScript = true;
							}
						}

						if (IsAddScript)
						{
							IAsyncResult gpcAsyncResult = pwsh.BeginInvoke();

							using (PSDataCollection<PSObject> ps_result = pwsh.EndInvoke(gpcAsyncResult))
							{
								foreach (PSObject psObject in ps_result)
								{
                                    // Manual(수동풀) 일시 psObject value == Null
                                    if (psObject != null)
                                    {
										DataRow row = _ret.NewRow();
										foreach (PSPropertyInfo prop in psObject.Properties)
										{
											if (prop != null)
											{
												if (prop.TypeNameOfValue.ToUpper().Contains("STRING[]"))
												{
													if (!_ret.Columns.Contains(prop.Name))
													{
														_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
													}
													string temp_prop_value = string.Empty;

													for (int temp_i = 0; prop.Value != null && temp_i < ((string[])prop.Value).Length; temp_i++)
													{
														temp_prop_value += ((string[])prop.Value)[temp_i] + ", ";
													}

													if (temp_prop_value.EndsWith(", "))
													{
														temp_prop_value = temp_prop_value.Substring(0, temp_prop_value.Length - 2);
													}

													row[prop.Name] = temp_prop_value;
												}
												else if (prop.TypeNameOfValue.ToUpper().Contains("DICTIONARY"))
												{
													if (!_ret.Columns.Contains(prop.Name))
													{
														_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
													}
													string temp_prop_value = string.Empty;

													row[prop.Name] = temp_prop_value;
												}
												else if (prop.TypeNameOfValue.ToUpper().Contains("NULLABLE"))
												{
													if (!_ret.Columns.Contains(prop.Name))
													{
														_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
													}

													try
													{
														row[prop.Name] = (prop.Value == null) ? "" : prop.Value;
													}
													catch
													{
														row[prop.Name] = "";
													}
												}
												else if (prop.TypeNameOfValue.ToUpper().Contains("INT"))
												{
													if (!_ret.Columns.Contains(prop.Name))
													{
														_ret.Columns.Add(new DataColumn(prop.Name, typeof(long)));
													}

													row[prop.Name] = prop.Value;
												}
												else
												{
													if (!_ret.Columns.Contains(prop.Name))
													{
														_ret.Columns.Add(new DataColumn(prop.Name, typeof(string)));
													}

													try
													{
														row[prop.Name] = (prop.Value == null) ? "" : prop.Value;
													}
													catch
													{
														row[prop.Name] = "";
													}
												}
											}
										}
										_ret.Rows.Add(row);
                                    }
								}
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				//Memory Leak
				try
				{
					runSpace = null;
					GC.Collect();
				}
				catch { }
			}

			return _ret;
		}

		public void Run(string Command)
		{
			string[] PrintCommandLines = Command.Split(new string[] { "\n" }, StringSplitOptions.None);

			//NCCFramework.Util.Logger.Debug(ref logger, $@"실행전 : Command = {string.Join("\r\n", PrintCommandLines)} / Now : {DateTime.Now}");

			try
			{
				using (runSpace = RunspaceFactory.CreateRunspace())
				{
					runSpace.Open();

					using (System.Management.Automation.PowerShell pwsh = System.Management.Automation.PowerShell.Create())
					{
						pwsh.Runspace = runSpace;
						pwsh.AddScript(Command);
						IAsyncResult gpcAsyncResult = pwsh.BeginInvoke();
						_ = pwsh.EndInvoke(gpcAsyncResult);
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				//Memory Leak
				try
				{
					runSpace = null;
					GC.Collect();
				}
				catch { }
			}
		}

		public void Run(List<string> Commands)
		{
			try
			{
				if (Commands?.Count > 0)
				{
					using (runSpace = RunspaceFactory.CreateRunspace())
					{
						runSpace.Open();

						using (System.Management.Automation.PowerShell pwsh = System.Management.Automation.PowerShell.Create())
						{
							pwsh.Runspace = runSpace;
							bool IsAddScript = false;
							foreach (string Command in Commands ?? Enumerable.Empty<string>())
							{
								if (!string.IsNullOrEmpty(Command))
								{
									pwsh.AddScript(Command);
									IsAddScript = true;
								}
							}

							if (IsAddScript)
							{
								IAsyncResult gpcAsyncResult = pwsh.BeginInvoke();
								_ = pwsh.EndInvoke(gpcAsyncResult);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				//Memory Leak
				try
				{
					runSpace = null;
					GC.Collect();
				}
				catch { }
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
					if (runSpace != null)
					{
						runSpace.Close();
						runSpace.Dispose();
						runSpace = null;
					}

					System.Threading.Thread.Sleep(1);
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		~PowerCli()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(false);
		}

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
