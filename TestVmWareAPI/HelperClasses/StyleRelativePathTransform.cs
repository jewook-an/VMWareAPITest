using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class StyleRelativePathTransform : IBundleTransform {
		private static Regex pattern = new Regex(@"url\s*\(\s*([""']?)([^:)]+)\1\s*\)", RegexOptions.IgnoreCase);

		public void Process(BundleContext context, BundleResponse response) {
			response.Content = string.Empty;

			foreach (BundleFile file in response.Files) {
				using (var reader = new StreamReader(file.VirtualFile.Open())) {
					var contents = reader.ReadToEnd();
					var matches = pattern.Matches(contents);

					if (matches.Count > 0) {
						var directoryPath = VirtualPathUtility.GetDirectory(file.VirtualFile.VirtualPath);

						foreach (Match match in matches) {
							var fileRelativePath = match.Groups[2].Value;
							var fileVirtualPath = VirtualPathUtility.Combine(directoryPath, fileRelativePath);
							var quote = match.Groups[1].Value;
							var replace = String.Format("url({0}{1}{0})", quote, VirtualPathUtility.ToAbsolute(fileVirtualPath));

							contents = contents.Replace(match.Groups[0].Value, replace);
						}

					}

					response.Content = String.Format("{0}\r\n{1}", response.Content, contents);
				}
			}
		}
	}
}