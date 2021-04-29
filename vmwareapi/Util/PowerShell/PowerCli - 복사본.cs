using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCRequireService.Util.PowerShell
{
	public class PowerCliTEST : IDisposable
	{
		System.Management.Automation.Runspaces.Runspace runSpace = null;

		private enum propType
		{
			Prop_ArrayString,
			Prop_Dictionary,
			Prop_Nullable,
			Prop_Int,
			Prop_Etc
		}

		public System.Data.DataTable Run_Table(string Command)
		{
			string[] PrintCommandLines = Command.Split(new string[] { "\n" }, StringSplitOptions.None);

			//NCCFramework.Util.Logger.Debug(ref logger, $@"실행전 : Command = {string.Join("\r\n", PrintCommandLines)} / Now : {DateTime.Now}");

			System.Data.DataTable _ret = new System.Data.DataTable();

			try
			{
				using (runSpace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace())
				{
					runSpace.Open();

					using (System.Management.Automation.PowerShell pwsh = System.Management.Automation.PowerShell.Create())
					{
						pwsh.Runspace = runSpace;
						pwsh.AddScript(Command);
						IAsyncResult gpcAsyncResult = pwsh.BeginInvoke();

						using (System.Management.Automation.PSDataCollection<System.Management.Automation.PSObject> ps_result = pwsh.EndInvoke(gpcAsyncResult))
						{
							foreach (System.Management.Automation.PSObject psObject in ps_result)
							{
								System.Data.DataRow row = _ret.NewRow();

								foreach (System.Management.Automation.PSPropertyInfo prop in psObject.Properties)
								{
									if (prop != null)
									{
										if (!_ret.Columns.Contains(prop.Name))
										{
											if (prop.TypeNameOfValue.ToUpper().Contains("INT"))
                                            {
												_ret.Columns.Add(new System.Data.DataColumn(prop.Name, typeof(long)));
                                            }
                                            else
                                            {
												_ret.Columns.Add(new System.Data.DataColumn(prop.Name, typeof(string)));
                                            }
										}

										if (prop.TypeNameOfValue.ToUpper().Contains("STRING[]"))
										{
											row[prop.Name] = rtnPropValue(propType.Prop_ArrayString, prop.Value);
										}
										else if (prop.TypeNameOfValue.ToUpper().Contains("DICTIONARY"))
										{
											row[prop.Name] = rtnPropValue(propType.Prop_Dictionary, prop.Value);
										}
										else if (prop.TypeNameOfValue.ToUpper().Contains("NULLABLE"))
										{
											row[prop.Name] = rtnPropValue(propType.Prop_Nullable, prop.Value);
										}
										else if (prop.TypeNameOfValue.ToUpper().Contains("INT"))
										{
											row[prop.Name] = prop.Value;
										}
										else
										{
											row[prop.Name] = rtnPropValue(propType.Prop_Etc, prop.Value);
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

        private string rtnPropValue(propType PropType, object propValue)
		{
			string temp_prop_value = string.Empty;

            try
            {
				switch (PropType)
				{
					case propType.Prop_ArrayString:
						for (int temp_i = 0; propValue != null && temp_i < ((string[])propValue).Length; temp_i++)
						{
							temp_prop_value += ((string[])propValue)[temp_i] + ", ";
						}

						if (temp_prop_value.EndsWith(", "))
						{
							temp_prop_value = temp_prop_value.Substring(0, temp_prop_value.Length - 2);
						}
						break;
					case propType.Prop_Dictionary:
						break;
					case propType.Prop_Nullable:
						temp_prop_value = (propValue == null) ? "" : propValue.ToString();
						break;
					case propType.Prop_Int:
						break;
					default:
						temp_prop_value = (propValue == null) ? "" : propValue.ToString();
						break;
				}
            }
            catch (Exception)
            {
                throw;
            }

			return temp_prop_value;
		}

        public System.Data.DataTable Run_Table(List<string> Commands)
		{
			System.Data.DataTable _ret = new System.Data.DataTable();

			try
			{
				using (runSpace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace())
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

							using (System.Management.Automation.PSDataCollection<System.Management.Automation.PSObject> ps_result = pwsh.EndInvoke(gpcAsyncResult))
							{
								foreach (System.Management.Automation.PSObject psObject in ps_result)
								{
									System.Data.DataRow row = _ret.NewRow();

									foreach (System.Management.Automation.PSPropertyInfo prop in psObject.Properties)
									{
										if (prop != null)
										{
											if (!_ret.Columns.Contains(prop.Name))
											{
												if (prop.TypeNameOfValue.ToUpper().Contains("INT"))
												{
													_ret.Columns.Add(new System.Data.DataColumn(prop.Name, typeof(long)));
												}
												else
												{
													_ret.Columns.Add(new System.Data.DataColumn(prop.Name, typeof(string)));
												}
											}

											if (prop.TypeNameOfValue.ToUpper().Contains("STRING[]"))
											{
												row[prop.Name] = rtnPropValue(propType.Prop_ArrayString, prop.Value);
											}
											else if (prop.TypeNameOfValue.ToUpper().Contains("DICTIONARY"))
											{
												row[prop.Name] = rtnPropValue(propType.Prop_Dictionary, prop.Value);
											}
											else if (prop.TypeNameOfValue.ToUpper().Contains("NULLABLE"))
											{
												row[prop.Name] = rtnPropValue(propType.Prop_Nullable, prop.Value);
											}
											else if (prop.TypeNameOfValue.ToUpper().Contains("INT"))
											{
												row[prop.Name] = prop.Value;
											}
											else
											{
												row[prop.Name] = rtnPropValue(propType.Prop_Etc, prop.Value);
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
				using (runSpace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace())
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
					using (runSpace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace())
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
		~PowerCliTEST()
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
