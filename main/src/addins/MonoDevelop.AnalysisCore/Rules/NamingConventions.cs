// 
// Adaptors.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2010 Novell, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using MonoDevelop.Projects.Dom;
using System.Collections.Generic;
using MonoDevelop.AnalysisCore.Fixes;
namespace MonoDevelop.AnalysisCore.Rules
{
	public static class NamingConventions
	{
		public static IEnumerable<Result> TypeNaming (IEnumerable<IType> input)
		{
			foreach (var type in input) {
				if ((type.ClassType == ClassType.Interface) && type.Name[0] != 'I') {
					var start = type.Location;
					var newName = "I" + char.ToUpper (type.Name[0]).ToString () + type.Name.Substring (1);
					yield return new FixableResult (
						new DomRegion (start, new DomLocation (start.Line, start.Column + type.Name.Length)),
						"Interface names should begin with an I",
						ResultLevel.Warning, ResultCertainty.High, ResultImportance.Medium,
						new RenameMemberFix (type, newName));
				}
				if (!char.IsUpper (type.Name[0])) {
					var start = type.Location;
					var newName = char.ToUpper (type.Name[0]).ToString () + type.Name.Substring (1);
					yield return new FixableResult (
						new DomRegion (start, new DomLocation (start.Line, start.Column + type.Name.Length)),
						"Type names should begin with an uppercase letter",
						ResultLevel.Warning, ResultCertainty.High, ResultImportance.Medium,
						new RenameMemberFix (type, newName));
				}
			}
		}
	}
}
