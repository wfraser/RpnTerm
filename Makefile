TARGETNAME=rpnterm.exe
TARGETTYPE=exe
TARGETDEST=.
TARGET=$(TARGETDEST)/$(TARGETNAME)

MCS=dmcs -debug+
SDK=4

SOURCES=\
    Properties/AssemblyInfo.cs \
    CalculatorStack.cs \
    Circularbuffer.cs \
    Display.cs \
    Enums.cs \
    ExtensionMethods.cs \
    Program.cs \
    Statics.cs

REFERENCES=

LIBDIR=.

ifeq ($(REFERENCES),)
	REFSFLAG=
else
	REFSFLAG=-reference:$(REFERENCES)
endif

all: $(TARGET)

$(TARGET): $(SOURCES)
	[ ! -x $(TARGETDEST) ] && mkdir $(TARGETDEST) || true
	$(MCS) -target:$(TARGETTYPE) -out:$(TARGET) -sdk:$(SDK) -lib:$(LIBDIR) $(REFSFLAG) $(DEFINES) $(SOURCES)

clean:
	rm -f $(TARGET) $(TARGET).mdb
