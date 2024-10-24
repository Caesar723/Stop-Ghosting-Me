#import <Foundation/Foundation.h>
#import <Cocoa/Cocoa.h>

extern "C" {

    NSWindow* GetMainWindow() {
        NSApplication* app = [NSApplication sharedApplication];
        return [app mainWindow];
    }

    void __attribute__((visibility("default"))) SetWindowPosition(float x, float y) {
        NSWindow* window = GetMainWindow();
        if (window) {
            NSRect frame = [window frame];
            frame.origin = NSMakePoint(x, y);
            [window setFrameOrigin:frame.origin];
        }
    }

    void __attribute__((visibility("default"))) GetWindowPosition(float* x, float* y) {
        NSWindow* window = GetMainWindow();
        if (window) {
            NSRect frame = [window frame];
            *x = frame.origin.x;
            *y = frame.origin.y;
        }
    }
    void __attribute__((visibility("default"))) GetCursorPosition(float* x, float* y)
    {
        NSWindow* window = GetMainWindow();
        if (window) {
            // 获取当前鼠标位置（全局坐标）
            NSPoint mouseLocation = [NSEvent mouseLocation];
            
            // macOS 的全局坐标是从左下角 (0,0) 开始的，而 Unity 使用的坐标是从左上角开始的。
            // 你可以根据需要调整坐标。
            *x = mouseLocation.x;
            *y = mouseLocation.y;
        }
    }

    float __attribute__((visibility("default"))) GetWindowBarHeight() {
        NSWindow* window = GetMainWindow();
        if (window) {
            NSRect contentRect = [window contentRectForFrameRect:[window frame]];
            return [window frame].size.height - contentRect.size.height;
        }
        return 0;
    }

    void __attribute__((visibility("default"))) SetWindowSize(float width, float height) {
        NSWindow* window = GetMainWindow();
        if (window) {
            NSRect frame = [window frame];
            frame.size = NSMakeSize(width, height);
            [window setFrame:frame display:YES];
        }

    }
    
}
