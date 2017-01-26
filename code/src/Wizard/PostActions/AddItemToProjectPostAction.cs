﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TemplateEngine.Edge.Template;
using Microsoft.Templates.Wizard.Vs;

namespace Microsoft.Templates.Wizard.PostActions
{
	public class AddItemToProjectPostAction : PostActionBase
	{
		public AddItemToProjectPostAction()
			: base("AddItemToProjectPostAction", "This post action adds the generated items to the project", null)		
		{
		}

		public override PostActionResult Execute(ExecutionContext context, TemplateCreationResult generationResult, IVisualStudioShell vsShell)
		{
			//TODO: Control overwrites! What happend if the generated content already exits.
			try
			{
				//TODO: Control multiple primary outputs, continue on failure or abort?
				foreach (var output in generationResult.PrimaryOutputs)
				{
					if (!string.IsNullOrWhiteSpace(output.Path))
					{
						var itemPath = Path.GetFullPath(Path.Combine(context.ProjectPath, context.PagesRelativePath, output.Path));
						vsShell.AddItemToActiveProject(itemPath);
					}
				}

				return new PostActionResult()
				{
					ResultCode = ResultCode.Success,
					Message = $"Postaction {Name}: Successfully added items to project"
				};
			}
			catch (Exception ex)
			{
				return new PostActionResult()
				{
					ResultCode = ResultCode.Error,
					Message = $"Postaction {Name}: Error adding items to project",
					Exception = ex
				};
			}
		}
	}
}
