import '@mdxeditor/editor/style.css';

import {
    AdmonitionDirectiveDescriptor,
    codeBlockPlugin,
    codeMirrorPlugin,
    diffSourcePlugin,
    DirectiveDescriptor,
    directivesPlugin,
    frontmatterPlugin,
    headingsPlugin,
    imagePlugin,
    KitchenSinkToolbar,
    linkDialogPlugin,
    linkPlugin,
    listsPlugin,
    markdownShortcutPlugin,
    quotePlugin,
    RealmPlugin,
    tablePlugin,
    thematicBreakPlugin,
    toolbarPlugin,
} from '@mdxeditor/editor';
import { basicDark } from 'cm6-theme-basic-dark';
import { LeafDirective } from 'mdast-util-directive';
import React from 'react';

interface YoutubeDirectiveNode extends LeafDirective {
    name: 'youtube';
    attributes: { id: string };
}

export const YoutubeDirectiveDescriptor: DirectiveDescriptor<YoutubeDirectiveNode> = {
    name: 'youtube',
    type: 'leafDirective',
    testNode(node) {
        return node.name === 'youtube';
    },
    attributes: ['id'],
    hasChildren: false,
    Editor: (p) => {
        return (
            <iframe
                style={{ width: '100%', height: 'auto', aspectRatio: '16 / 9' }}
                src={`https://www.youtube.com/embed/${p.mdastNode.attributes.id}`}
                title="YouTube video player"
                frameBorder="0"
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
            ></iframe>
        );
    },
};

export const GetAllPlugins = (isDark?: boolean, isEdit?: boolean, diffMd?: string) => {
    const plugs: RealmPlugin[] = [];

    plugs.push(listsPlugin());
    plugs.push(quotePlugin());
    plugs.push(headingsPlugin({ allowedHeadingLevels: [1, 2, 3, 4, 5] }));
    plugs.push(linkPlugin());
    plugs.push(linkDialogPlugin());
    plugs.push(imagePlugin());
    plugs.push(tablePlugin());
    plugs.push(frontmatterPlugin());
    plugs.push(thematicBreakPlugin());
    plugs.push(codeBlockPlugin({ defaultCodeBlockLanguage: '' }));
    plugs.push(
        codeMirrorPlugin({
            codeBlockLanguages: {
                txt: 'Plain Text',
                '': 'Unspecified',
            },
            codeMirrorExtensions: isDark ? [basicDark] : [],
        }),
    );
    plugs.push(
        directivesPlugin({
            directiveDescriptors: [
                YoutubeDirectiveDescriptor,
                AdmonitionDirectiveDescriptor,
            ],
        }),
    );

    if (isEdit) {
        plugs.push(
            diffSourcePlugin({
                viewMode: 'rich-text',
                diffMarkdown: diffMd,
                codeMirrorExtensions: isDark ? [basicDark] : [],
            }),
        );
        plugs.push(
            toolbarPlugin({
                toolbarContents: () => <KitchenSinkToolbar />,
            }),
        );
        plugs.push(markdownShortcutPlugin());
    }

    return plugs;
};
