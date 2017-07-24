//===============================================================================
// Microsoft patterns & practices
// Prism 4 Utilities
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ManifestManagerUtility
{
   public partial class AboutForm : Form
   {
      public AboutForm()
      {
         InitializeComponent();
      }

      private void buttonOk_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void OnGotoIDesign(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://www.idesign.net");
      }

	   private void OnGotoPatternsAndPractices(object sender, LinkLabelLinkClickedEventArgs e)
	   {
		   Process.Start("http://www.msdn.com/practices");
	   }
   }
}