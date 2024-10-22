import { syncinposTemplatePage } from './app.po';

describe('syncinpos App', function() {
  let page: syncinposTemplatePage;

  beforeEach(() => {
    page = new syncinposTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
