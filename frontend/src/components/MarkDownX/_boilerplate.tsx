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
            <div
                style={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'flex-start',
                }}
            >
                <button
                    onClick={() => {
                        p.parentEditor.update(() => {
                            p.lexicalNode.selectNext();
                            p.lexicalNode.remove();
                        });
                    }}
                >
                    delete
                </button>
                <iframe
                    width="560"
                    height="315"
                    src={`https://www.youtube.com/embed/${p.mdastNode.attributes.id}`}
                    title="YouTube video player"
                    frameBorder="0"
                    allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                ></iframe>
            </div>
        );
    },
};

export const GetAllPlugins = (diffMd: string, isDark?: boolean) => [
    toolbarPlugin({ toolbarContents: () => <KitchenSinkToolbar /> }),
    listsPlugin(),
    quotePlugin(),
    headingsPlugin({ allowedHeadingLevels: [1, 2, 3, 4, 5] }),
    linkPlugin(),
    linkDialogPlugin(),
    imagePlugin(),
    tablePlugin(),
    thematicBreakPlugin(),
    frontmatterPlugin(),
    codeBlockPlugin({ defaultCodeBlockLanguage: '' }),
    codeMirrorPlugin({
        codeBlockLanguages: {
            js: 'JavaScript',
            css: 'CSS',
            txt: 'Plain Text',
            tsx: 'TypeScript',
            '': 'Unspecified',
        },
        codeMirrorExtensions: isDark ? [basicDark] : [],
    }),
    directivesPlugin({
        directiveDescriptors: [YoutubeDirectiveDescriptor, AdmonitionDirectiveDescriptor],
    }),
    diffSourcePlugin({
        viewMode: 'rich-text',
        diffMarkdown: diffMd,
        codeMirrorExtensions: isDark ? [basicDark] : [],
    }),
    markdownShortcutPlugin(),
];
