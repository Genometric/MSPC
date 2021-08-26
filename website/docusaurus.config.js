module.exports = {
  title: 'MSPC',
  tagline: 'Using combined evidence from replicates to evaluate ChIP-seq peaks',
  url: 'https://genometric.github.io',
  baseUrl: '/MSPC/',
  favicon: 'logo/logo.svg',
  organizationName: 'Genometric',
  projectName: 'MSPC',
  themeConfig: {
    hideableSidebar: true,
    navbar: {
      title: 'MSPC',
      logo: {
        alt: 'logo',
        src: 'logo/logo.svg',
      },
      items: [
        {
          to: 'docs/',
          activeBasePath: 'docs',
          label: 'Documentation',
          position: 'right',
        },
		{
			label: 'Source Code',
			href: 'https://github.com/Genometric/MSPC/',  
			position: 'right'
		},
		{
			label: 'Download',
			href: 'https://github.com/Genometric/MSPC/releases',  
			position: 'right'
		},
		{
			label: 'Questions',
			href: 'https://github.com/Genometric/MSPC/issues',  
			position: 'right'
		},
		{
			label: 'Publications',
			to: 'Publications',  
			position: 'right'
		}
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            {
              label: 'Quick Start',
              to: 'docs/quick_start/',
            },
            {
              label: 'Installation',
              to: 'docs/installation/',
            },
          ],
        },
        {
          title: 'Community',
          items: [
            {
              label: 'Github',
              href: 'https://github.com/Genometric/MSPC',
            }
          ],
        }
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Genometric`,
    },
    algolia: {
      // This is a public API key. This key is only usable for 
      // search queries and sending data to the Insights API.
      apiKey: 'aab79977ea094db4ed98dba66a22dd42', 
      indexName: 'mspc',
      // appId: '7KY7XPQTMJ',
      contextualSearch: true,
      // searchParameter: {} // Optional
    },
  },
  presets: [
    [
      '@docusaurus/preset-classic',
      {
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          editUrl:
            'https://github.com/Genometric/MSPC/tree/dev/website',
        },
        /*
		blog: {
          showReadingTime: true,
          editUrl: '...',
        },
		*/
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      },
    ],
  ]
};
