###############################################################################
############################################## Makefile_TAG (200729:17h:33) ###
###############################################################################
#{{{
 TOP            = $(LOCAL)/STORE/DEV/HARDWARE/DX1
 ARCHDIR        = $(LOCAL)/STORE/DEV/HARDWARE/ARCHIVES
 APPDIR         = DX1Utility
 FIND           = find.exe
 FTP            = c:/WINDOWS/system32/ftp.exe
 JAR            = jar.exe
 LS             = ls.exe
 RAR            = rar.exe
 RAS            = -av- -c- -cfg- -ed -idp -inul -tl
#}}}
:         make clean arch
:         make wc
:         make clean
:         make ftp
:cd %:h|up|only|vert terminal make tags
:cd %:h|up|only|vert terminal make clean arch
:cd %:h|up|only|vert terminal make ftp
default: clean arch tags
arch:                                                                     #{{{
	@(\
	    echo "[$@] in $(TOP)";\
	    cd $(TOP);\
	    pwd;\
	    echo "APPDIR=[$(APPDIR)]";\
	    FILES=`$(FIND) $(APPDIR) -type f\
	    | sed                       \
	    -e "s/ /?/g"                \
	    \
	    -e "/\.application/d"       \
	    -e "/\.dll/d"               \
	    -e "/\.exe*/d"              \
	    -e "/\.git*/d"              \
	    -e "/\.lnk/d"               \
	    -e "/\.orig/d"              \
	    -e "/\.pdb/d"               \
	    -e "/\.psd/d"               \
	    -e "/\.rar*/d"              \
	    -e "/\.suo*/d"              \
	    -e "/\/LINKS/d"             \
	    -e "/\/bin.*/d"             \
	    -e "/\\obj.*/d"             \
	    -e "/tags/d"                \
	    \
	    `;\
	    DATE=`date '+%y%m%d'`;\
	    ARCHIVE="$(APPDIR)_$$DATE.rar";\
	    echo $(RAR)   a $(RAS) $(ARCHDIR)/$$ARCHIVE $$FILES;\
		 $(RAR)   a $(RAS) $(ARCHDIR)/$$ARCHIVE $$FILES;\
	    $(LS)       -lt        $(ARCHDIR)/*.rar | head;\
#           $(RAR)       vt        $(ARCHDIR)/$$ARCHIVE;\
	)#

# {{{
# ---------------------------------------------------------------------
# }!!rar -?
# 
# ---------------------------------------------------------------------
# $(RAR)   a $(RAS) $(ARCHDIR)/$$ARCHIVE $$FILES;\
# $(RAR)   lv       $(ARCHDIR)/$$ARCHIVE;\
# ---------------------------------------------------------------------
# Usage:     rar <command> -<switch 1> -<switch N> <archive> <files...>
# <Commands>
#  v[t,b]        Verbosely list archive [technical,bare]
# <Switches>
#   av-           Disable authenticity verification check
#   c-            Disable comments show
#   cfg-          Disable read configuration
#   ed            Do not add empty directories
#   idp           Disable percentage display
#   inul          Disable all messages
#   tl            Set archive time to latest file
# ---------------------------------------------------------------------
# }}}
# }}}
ftp:                                                                      #{{{
	@(\
	echo "[$@] in [$(APPDIR)]";\
	cd $(APPDIR);\
	pwd;\
	FTPR_PUT_FILE=ftp_remotetabs_PUT.txt;\
	$(FTP) -n -i -w:1024 -s:$$FTPR_PUT_FILE;\
	)
# }}}
clean:                                                                    #{{{
	@(\
	echo "[$@] in $(TOP)";\
	cd $(TOP);\
        set -x;\
	rm -rf tags */tags */*/tags;\
	rm -rf .vs */.vs obj */obj/;\
	)

# }}}
wc:                                                                       #{{{
	@(\
	echo "[$@]";\
	wc -l `find . -name '*.cs'` | sort -n;\
	)

# }}}
.PHONY: tags                                                              #{{{
tags:
	cd $(TOP) && ctags -R --links=no $(APPDIR) && wc -l tags

#}}}
#vim: noexpandtab 

