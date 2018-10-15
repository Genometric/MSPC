/**
 * Copyright (c) 2017-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

// See https://docusaurus.io/docs/site-config for all the possible
// site configuration options.


const siteConfig = {
  title: 'MSPC', // Title for your website.
  tagline: 'Using combined evidence from replicates to evaluate ChIP-seq peaks',
  url: 'https://genometric.github.io', // Your website URL
  baseUrl: '/MSPC/', // Base URL for your project */
  // For github.io type URLs, you would set the url and baseUrl like:
  //   url: 'https://facebook.github.io',
  //   baseUrl: '/test-site/',

  // Used for publishing and more
  projectName: 'MSPC',
  organizationName: 'Genometric',
  // For top-level user or org sites, the organization is still the same.
  // e.g., for the https://JoelMarcey.github.io site, it would be set like...
  //   organizationName: 'JoelMarcey'

  // For no header links in the top nav bar -> headerLinks: [],
  headerLinks: [
    {label: 'Documentation', doc: 'welcome'},
    {label: 'Download', href: 'https://github.com/Genometric/MSPC/releases'},
    {label: 'Questions', href: 'https://github.com/Genometric/MSPC/issues'},
  ],

  /* path to images for header/footer */
  headerIcon: 'img/mspc_logo.svg',
  footerIcon: 'img/mspc_logo.svg',
  favicon: 'img/favicon.png',

  /* Colors for website */
  colors: {
    primaryColor: '#13001e', // '#321730',
    secondaryColor: '#96af4c',
  },

  /* Custom fonts for website */
  /*
  fonts: {
    myFont: [
      "Times New Roman",
      "Serif"
    ],
    myOtherFont: [
      "-apple-system",
      "system-ui"
    ]
  },
  */

  // This copyright info is used in /core/Footer.js and blog RSS/Atom feeds.
  copyright: `Copyright © ${new Date().getFullYear()} Genometric`,

  highlight: {
    // Highlight.js theme to use for syntax highlighting in code blocks.
    // theme: 'dracula', 
    theme: 'sunburst',
  },

  // Add custom scripts here that would be placed in <script> tags.
  scripts: ['https://buttons.github.io/buttons.js'],

  // On page navigation for the current documentation page.
  onPageNav: 'separate',
  // No .html extensions for paths.
  cleanUrl: true,

  // You may provide arbitrary config keys to be used as needed by your
  // template. For example, if you need your repo's URL...
  //   repoUrl: 'https://github.com/facebook/test-site',
};

module.exports = siteConfig;
