module.exports = {
  title: 'MSPC',
  tagline: 'Using combined evidence from replicates to evaluate ChIP-seq peaks',
  url: 'https://genometric.github.io',
  baseUrl: '/MSPC/',
  favicon: 'img/logo.png',
  organizationName: 'Genometric',
  projectName: 'MSPC',
  themeConfig: {
    navbar: {
      title: 'MSPC',
      logo: {
        alt: 'MSPC Logo',
        src: 'img/logo.svg',
      },
      links: [
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
  },
  presets: [
    [
      '@docusaurus/preset-classic',
      {
        docs: {
          homePageId: 'welcome',
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
  ],
};
