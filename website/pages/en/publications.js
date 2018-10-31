/**
 * Copyright (c) 2017-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

const React = require('react');

const CompLibrary = require('../../core/CompLibrary.js');

const MarkdownBlock = CompLibrary.MarkdownBlock; /* Used to read markdown */
const Container = CompLibrary.Container;
const GridBlock = CompLibrary.GridBlock;

const siteConfig = require(`${process.cwd()}/siteConfig.js`);

const SplashContainer = props => (
  <div className="homeContainer">
    <div className="homeSplashFade">
      <div className="wrapper homeWrapper">{props.children}</div>
    </div>
  </div>
);

const ProjectTitle = () => (
  <h2 className="projectTitle">
    {siteConfig.title}
    <small>{siteConfig.tagline}</small>
  </h2>
);

const PromoSection = props => (
  <div className="section promoSection">
    <div className="promoRow">
      <div className="pluginRowBlock">{props.children}</div>
    </div>
  </div>
);

class HomeSplash extends React.Component {
  render() {
    const language = this.props.language || '';
    return (
      <SplashContainer>
        <div className="inner">
          <ProjectTitle />
          <PromoSection>
          </PromoSection>
        </div>
      </SplashContainer>
    );
  }
}

const Block = props => (
  <Container
    padding={['bottom', 'top']}
    id={props.id}
    background={props.background}>
    <GridBlock align="center" contents={props.children} layout={props.layout} />
  </Container>
);

const P1 = () => (
  <div
    className="container paddingBottom"
    style={{textAlign: 'left'}}>
    <span class="__dimensions_badge_embed__" data-doi="10.1093/bioinformatics/btv293"></span>
    <div class='altmetric-embed' data-badge-type='donut' data-doi="10.1093/bioinformatics/btv293"></div>
    <h4>Jalili, V., Matteucci, M., Masseroli, M., & Morelli, M. J. (2015). Using combined evidence from replicates to evaluate ChIP-seq peaks. Bioinformatics, 31(17), 2761-2769.</h4>
    
    <MarkdownBlock>[Web](https://academic.oup.com/bioinformatics/article/31/17/2761/183989)</MarkdownBlock>
  </div>
);

const P2 = () => (
  <div
    className="container paddingBottom"
    style={{textAlign: 'left'}}>
    <span class="__dimensions_badge_embed__" data-doi="10.1093/bib/bbw029"></span>
    <div class='altmetric-embed' data-badge-type='donut' data-doi="10.1093/bib/bbw029"></div>
    <h4>Jalili, V., Matteucci, M., Morelli, M. J., & Masseroli, M. (2016). MuSERA: multiple sample enriched region assessment. Briefings in bioinformatics, 18(3), 367-381.</h4>
    
    <MarkdownBlock>[Web](https://academic.oup.com/bib/article-abstract/18/3/367/2562755)</MarkdownBlock>
  </div>
);


class Index extends React.Component {
  render() {
    const language = this.props.language || '';

    return (
      <div>
        <HomeSplash language={language} />
        <div className="mainContainer">
          <P1 />
          <P2 />
        </div>
      </div>
    );
  }
}

module.exports = Index;
